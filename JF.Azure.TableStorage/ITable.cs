using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace JF.Azure.TableStorage {

	/// <summary>
	///     The cloud Table wrapper, it handles basic operations
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ITable<T>
		where T : ITableEntity {

		/// <summary>
		///     Inserts or replaces an entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		TableResult AddOrUpdate( T entity );

		/// <summary>
		///     Inserts or replaces eneities
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		IList<TableResult> AddOrUpdate( IList<T> entities );

		/// <summary>
		///     Inserts or replaces an entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task<TableResult> AddOrUpdateAsync( T entity );

		/// <summary>
		///     Inserts or replaces eneities
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		Task<IList<TableResult>> AddOrUpdateAsync( IList<T> entities );

		/// <summary>
		///     Finds an entity by a TableQuery "where" filter
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		IList<T> FindBy( string filter );

		/// <summary>
		///     Searches entities by a propertyname for an exact match
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		IList<T> FindBy( string propertyName , string value );

		/// <summary>
		///     Finds entities by their row key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		IList<T> FindByRowKey( string key );

		/// <summary>
		///     Finds entities by their partition key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		IList<T> FindByPartitionKey( string key );

		/// <summary>
		///     Finds a single entity by the row and partition key
		/// </summary>
		/// <param name="partitionKey"></param>
		/// <param name="rowKey"></param>
		/// <returns></returns>
		T Find( string partitionKey , string rowKey );

		/// <summary>
		///     Removes a single entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		TableResult Remove( T entity );

		/// <summary>
		///     Removes a single entity identified by the row and partition key
		/// </summary>
		/// <param name="partitionKey"></param>
		/// <param name="rowKey"></param>
		/// <returns></returns>
		TableResult Remove( string partitionKey , string rowKey );

		/// <summary>
		///     Removes a single entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task<TableResult> RemoveAsync( T entity );

		/// <summary>
		///     Removes a single entity identified by the row and partition key
		/// </summary>
		/// <param name="partitionKey"></param>
		/// <param name="rowKey"></param>
		/// <returns></returns>
		Task<TableResult> RemoveAsync( string partitionKey , string rowKey );

		/// <summary>
		///     Removes a list of entities
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		IList<TableResult> RemoveRange( List<T> entities );

		/// <summary>
		///     Removes a list of entities
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		Task<IList<TableResult>> RemoveRangeAsync( List<T> entities );

	}

}
