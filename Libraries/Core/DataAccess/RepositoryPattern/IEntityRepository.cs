using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.RepositoryPattern
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {

        /// <summary>
        /// Bir kaydı getirir.
        /// </summary>
        /// <param name="predicate">Koşula göre filtreler</param>
        /// <param name="include">Include yapar.</param>
        /// <param name="ignoreQueryFilters">QueryFilter özelliğini devreye sokup/çıkarıtr. <c>true</c> verilirse devreden çıkar. Varsayılan: <c>false</c>, IgnoreQueryFilters devrede.</param>
        /// <param name="disableTracking">Tracking mekanizmasını kapatıp/açar. <c>true</c> yapılırsa AsNoTracking devreye girer. Varsayılan: <c>false</c> tracking aktif.</param>
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool ignoreQueryFilters = false,
            bool disableTracking = false);


        /// <summary>
        /// Kayıtları getirir.
        /// </summary>
        /// <param name="predicate">Koşula göre filtreler</param>
        /// <param name="include">Include yapar.</param>
        /// <param name="ignoreQueryFilters">QueryFilter özelliğini devreye sokup/çıkarıtr. <c>true</c> verilirse devreden çıkar. Varsayılan: <c>false</c>, IgnoreQueryFilters devrede.</param>
        /// <param name="disableTracking">Tracking mekanizmasını kapatıp/açar. <c>true</c> yapılırsa AsNoTracking devreye girer. Varsayılan: <c>false</c> tracking aktif.</param>
        Task<List<TEntity>> GetAllAsync(
             Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool ignoreQueryFilters = false,
            bool disableTracking = false);

        /// <summary>
        /// Bir verinin kayıtlı olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="predicate">Filtre</param>
        /// <param name="include">Include yapar.</param>
        /// <param name="ignoreQueryFilters">QueryFilter özelliğini devreye sokup/çıkarıtr. <c>false</c> ise devrededir.</param>
        /// <returns>Kayıt varsa <c>TRUE</c> döner.</returns>
        Task<bool> IsExistsAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// Kayıtlı verilerin sayısını getirir.
        /// </summary>
        /// <param name="predicate">Filtre</param>
        /// <param name="include">Include yapar.</param>
        /// <param name="ignoreQueryFilters">QueryFilter özelliğini devreye sokup/çıkarıtr. <c>false</c> ise devrededir.</param>
        /// <returns>Veri sayısı</returns>
        Task<int> CountAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool ignoreQueryFilters = false);


        /// <summary>
        /// Veri kayıt eder.
        /// </summary>
        /// <returns>Kayıtlar eklenirse true döner.</returns>
        Task<bool> AddAsync(TEntity entity);

        /// <summary>
        /// Birden fazla kayıtı aynı anda ekler.
        /// </summary>
        /// <returns>Kayıtlar eklenirse true döner.</returns>
        Task<bool> AddBulkAsync(List<TEntity> entities);

        /// <summary>
        /// Veriyi günceller.
        /// </summary>
        /// <returns>Kayıt güncellenirse true döner.</returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Veriyi tamamen siler.
        /// </summary>
        /// <returns>Kayıt silinirse true döner.</returns>
        Task<bool> HardDeleteAsync(TEntity entity);
    }
}
