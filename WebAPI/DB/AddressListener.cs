using Microsoft.Data.Sqlite;
using SQLitePCL;
using SqlSugar;

namespace WebAPI.DB
{
    public class AddressListener
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; } //主键
        public string Url { get; set; } //监听地址

        /// <summary>
        /// 依据id获取监听地址
        /// </summary>
        /// <param name="id"></param>
        public AddressListener(int id)
        {
            var connectionString = new SqliteConnectionStringBuilder()
            {
                DataSource = "..\\WebAPI\\DB\\Address.db",
            }.ToString();
            SqlSugarClient client = new(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                IsAutoCloseConnection = true,
                DbType = DbType.Sqlite,
            });

            Id = id;
            Url = client.Queryable<AddressListener>().Where(it => it.Id == id).Select(it => it.Url).First();
        }
    }
}
