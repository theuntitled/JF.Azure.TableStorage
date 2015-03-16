using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace JF.Azure.TableStorage {

	/// <summary>
	///     The cloud Table wrapper, it handles basic operations
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Table<T> : ITable<T>
		where T : class , ITableEntity , new() {

		/// <summary>
		///     The actual Microsoft.WindowsAzure.Storage.Table.CloudTable instance
		/// </summary>
		protected CloudTable CloudTable { get; set; }

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="cloudTable"></param>
		public Table( CloudTable cloudTable ) {
			CloudTable = cloudTable;
		}

		/// <summary>
		///     Inserts or replaces an entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public TableResult AddOrUpdate( T entity ) {
			return CloudTable.Execute( TableOperation.InsertOrReplace( entity ) );
		}

		/// <summary>
		///     Inserts or replaces eneities
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		public IList<TableResult> AddOrUpdate( IList<T> entities ) {
			var batch = new TableBatchOperation();

			foreach ( var entity in entities ) {
				batch.InsertOrReplace( entity );
			}

			return CloudTable.ExecuteBatch( batch );
		}

		/// <summary>
		///     Inserts or replaces an entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public Task<TableResult> AddOrUpdateAsync( T entity ) {
			return CloudTable.ExecuteAsync( TableOperation.InsertOrReplace( entity ) );
		}

		/// <summary>
		///     Inserts or replaces eneities
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		public Task<IList<TableResult>> AddOrUpdateAsync( IList<T> entities ) {
			var batch = new TableBatchOperation();

			foreach ( var entity in entities ) {
				batch.InsertOrReplace( entity );
			}

			return CloudTable.ExecuteBatchAsync( batch );
		}

		/// <summary>
		///     Finds an entity by a TableQuery "where" filter
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		public IList<T> FindBy( string filter ) {
			var query = new TableQuery<T>().Where( filter );

			return CloudTable.ExecuteQuery( query ).ToList();
		}

		/// <summary>
		///     Searches entities by a propertyname for an exact match
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public IList<T> FindBy( string propertyName , string value ) {
			return FindBy( TableQuery.GenerateFilterCondition( propertyName , QueryComparisons.Equal , value ) );
		}

		/// <summary>
		///     Finds entities by their row key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public IList<T> FindByRowKey( string key ) {
			return FindBy( TableQuery.GenerateFilterCondition( "RowKey" , QueryComparisons.Equal , key ) );
		}

		/// <summary>
		///     Finds entities by their partition key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public IList<T> FindByPartitionKey( string key ) {
			return FindBy( TableQuery.GenerateFilterCondition( "PartitionKey" , QueryComparisons.Equal , key ) );
		}

		/// <summary>
		///     Finds a single entity by the row and partition key
		/// </summary>
		/// <param name="partitionKey"></param>
		/// <param name="rowKey"></param>
		/// <returns></returns>
		public T Find( string partitionKey , string rowKey ) {
			var result = CloudTable.Execute( TableOperation.Retrieve<T>( partitionKey , rowKey ) );

			if ( result.Result == null ) {
				return null;
			}

			return result.Result as T;
		}

		/// <summary>
		///     Removes a single entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public TableResult Remove( T entity ) {
			return Remove( entity.PartitionKey , entity.RowKey );
		}

		/// <summary>
		///     Removes a single entity identified by the row and partition key
		/// </summary>
		/// <param name="partitionKey"></param>
		/// <param name="rowKey"></param>
		/// <returns></returns>
		public TableResult Remove( string partitionKey , string rowKey ) {
			var entityToDelete = Find( partitionKey , rowKey );

			if ( entityToDelete == null ) {
				return null;
			}

			return CloudTable.Execute( TableOperation.Delete( entityToDelete ) );
		}

		/// <summary>
		///     Removes a single entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public Task<TableResult> RemoveAsync( T entity ) {
			return RemoveAsync( entity.PartitionKey , entity.RowKey );
		}

		/// <summary>
		///     Removes a single entity identified by the row and partition key
		/// </summary>
		/// <param name="partitionKey"></param>
		/// <param name="rowKey"></param>
		/// <returns></returns>
		public Task<TableResult> RemoveAsync( string partitionKey , string rowKey ) {
			var entityToDelete = Find( partitionKey , rowKey );

			if ( entityToDelete == null ) {
				return null;
			}

			return CloudTable.ExecuteAsync( TableOperation.Delete( entityToDelete ) );
		}

		/// <summary>
		///     Removes a list of entities
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		public IList<TableResult> RemoveRange( List<T> entities ) {
			var batch = new TableBatchOperation();

			foreach ( var entity in entities ) {
				batch.Delete( entity );
			}

			return CloudTable.ExecuteBatch( batch );
		}

		/// <summary>
		///     Removes a list of entities
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		public Task<IList<TableResult>> RemoveRangeAsync( List<T> entities ) {
			var batch = new TableBatchOperation();

			foreach ( var entity in entities ) {
				batch.Delete( entity );
			}

			return CloudTable.ExecuteBatchAsync( batch );
		}

	}

}
