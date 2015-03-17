using System;
using System.Linq;
using System.Reflection;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace JF.Azure.TableStorage {

	/// <summary>
	///     The abstract Table manager that intializes the database when needed
	/// </summary>
	public abstract class TableManager : ITableManager {

		/// <summary>
		///     The actual cloud table client
		/// </summary>
		protected readonly CloudTableClient TableClient;

		/// <summary>
		///     The actual cloud storage account
		/// </summary>
		protected readonly CloudStorageAccount StorageAccount;

		private readonly Type _iTableType = typeof (ITable<>);

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="connectionStringName"></param>
		/// <exception cref="ArgumentNullException"></exception>
		protected TableManager( string connectionStringName ) {
			if ( string.IsNullOrEmpty( connectionStringName ) ) {
				throw new ArgumentNullException( "connectionStringName" );
			}

			StorageAccount = CloudStorageAccount.Parse( CloudConfigurationManager.GetSetting( connectionStringName ) );
			TableClient = StorageAccount.CreateCloudTableClient();

			Initialize();
		}

		/// <summary>
		///     Checks if the property is an ITable
		/// </summary>
		/// <param name="propertyInfo"></param>
		/// <returns></returns>
		protected virtual bool IsTable( PropertyInfo propertyInfo ) {
			return propertyInfo.PropertyType.IsClass &&
				   propertyInfo.PropertyType.GetInterfaces()
					   .Any( interfaceInfo => interfaceInfo.IsGenericType && interfaceInfo.GetGenericTypeDefinition() == _iTableType );
		}

		/// <summary>
		///     Sets up the models and ITable instances
		/// </summary>
		protected void Initialize() {
			var properties = GetType().GetProperties().Where( IsTable ).ToList();

			foreach ( var property in properties ) {
				var cloudTable = TableClient.GetTableReference( property.Name );

				var table = Activator.CreateInstance( property.PropertyType , cloudTable );

				cloudTable.CreateIfNotExists();

				property.SetValue( this , table );
			}
		}

		/// <summary />
		public void Dispose() {
		}

	}

}
