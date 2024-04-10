using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newwwrpi.utils
{
    public class DBHelper
    {
        public static object nid = 0;
        public static List<string> SelectValuesFromTable(string tableName, int id)
        {
            Console.WriteLine(tableName);
            var result = new List<string>();

            var conn = new SqlConnection(Properties.Settings.Default.shopConnectionString);
            var query = $"SELECT * FROM {tableName} WHERE {Constants.FieldNames.Id} = {id}";
            var cmd = new SqlCommand(query, conn);
            conn.Open();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Clear();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    result.Add(reader[i].ToString());
                }
            }
            return result;
        }

        public static List<string> SelectValuesFromSpec(string tableName, int id)
        {
            Console.WriteLine(tableName);
            var result = new List<string>();

            var conn = new SqlConnection(Properties.Settings.Default.shopConnectionString);
            var query = $"SELECT * FROM {tableName} WHERE {Constants.FieldNames.specificationsTable.clother_id} = {id}";
            var cmd = new SqlCommand(query, conn);
            conn.Open();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Clear();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    result.Add(reader[i].ToString());
                }
            }
            return result;
        }
        public static bool DeleteEntry(string tableName, int id)
        {
            try
            {
                var conn = new SqlConnection(Properties.Settings.Default.shopConnectionString);
                var query = $"DELETE FROM {tableName} WHERE id = {id}";
                var cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public static bool InsertEntry(string tableName, Dictionary<string, TableField> fields)
        {
            try
            {
                var conn = new SqlConnection(Properties.Settings.Default.shopConnectionString);
                var fieldsNames = string.Join(",", fields.Select(f => f.Key));
                var fieldsValues = string.Join(",", fields.Select(f =>
                {
                    if (f.Value.TableFieldType == TableFieldTypes.integer)
                    {
                        return f.Value.TableFieldValue;
                    }
                    return $"'{f.Value.TableFieldValue}'";
                }));
                var query = $"INSERT INTO {tableName} ({fieldsNames}) VALUES ({fieldsValues}) SELECT @@IDENTITY";
                Console.WriteLine(query);
                var cmd = new SqlCommand(query, conn);
                conn.Open();
                //var nid = cmd.ExecuteNonQuery();
                nid = cmd.ExecuteScalar();
                Console.WriteLine(nid.ToString());
                conn.Close();
                return true;
            }
            catch { return false; }
        }
        public static bool UpdateEntry(string tableName, int id, Dictionary<string, TableField> fields)
        {
            try
            {
                var conn = new SqlConnection(Properties.Settings.Default.shopConnectionString);

                var updatingFieldsValues = string.Join(", ", fields.Select(f => {
                    var fieldValue = string.Empty;
                    if (f.Value.TableFieldType == TableFieldTypes.integer)
                    {
                        fieldValue = f.Value.TableFieldValue;

                    }
                    else
                    {
                        fieldValue = $"'{f.Value.TableFieldValue}'";
                    }
                    return $"{f.Key}={fieldValue}";
                }));
                
                var query = $"UPDATE {tableName} SET {updatingFieldsValues} WHERE {Constants.FieldNames.Id}={id}";
                Console.WriteLine(query);
                var cmd = new SqlCommand(query, conn);               
                conn.Open();
                cmd.ExecuteNonQuery();
                
                conn.Close();

                return true;


            }
            catch
            {
                Console.WriteLine("333");
                return false;
            }
        }

        public static bool UpdateEntrySpec(string tableName, int id, Dictionary<string, TableField> fields)
        {
            try
            {
                var conn = new SqlConnection(Properties.Settings.Default.shopConnectionString);

                var updatingFieldsValues = string.Join(", ", fields.Select(f => {
                    var fieldValue = string.Empty;
                    if (f.Value.TableFieldType == TableFieldTypes.integer)
                    {
                        fieldValue = f.Value.TableFieldValue;

                    }
                    else
                    {
                        fieldValue = $"'{f.Value.TableFieldValue}'";
                    }
                    return $"{f.Key}={fieldValue}";
                }));

                var query = $"UPDATE {tableName} SET {updatingFieldsValues} WHERE {Constants.FieldNames.specificationsTable.clother_id}={id}";
                var cmd = new SqlCommand(query, conn);
                Console.WriteLine("спецфилд:");
                Console.WriteLine(updatingFieldsValues);
                Console.WriteLine(Constants.FieldNames.specificationsTable.clother_id);
                Console.WriteLine(id);
                Console.WriteLine($"UPDATE {tableName} SET {updatingFieldsValues} WHERE {Constants.FieldNames.specificationsTable.clother_id}={id}");

                conn.Open();
                Console.WriteLine("777");
                cmd.ExecuteNonQuery();
                Console.WriteLine("000");
                conn.Close();

                return true;


            }
            catch
            {
                Console.WriteLine("333");
                return false;
            }


        }

        public static bool InsertEntrySpec(string tableName, Dictionary<string, TableField> fields)
        {
            try
            {
                var conn = new SqlConnection(Properties.Settings.Default.shopConnectionString);
                var fieldsNames = string.Join(",", fields.Select(f => f.Key));
                var fieldsValues = string.Join(",", fields.Select(f =>
                {
                    if (f.Value.TableFieldType == TableFieldTypes.integer)
                    {
                        return f.Value.TableFieldValue;
                    }
                    return $"'{f.Value.TableFieldValue}'";
                }));
                var query = $"INSERT INTO specifications ({fieldsNames}) VALUES ({fieldsValues})";
                Console.WriteLine(query);
                var cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch { return false; }
        }
    }



    public class TableField
    {
        public string TableFieldValue { get; set; }
        public TableFieldTypes TableFieldType { get; set; }
    }

    public enum TableFieldTypes
    {
        integer,
        varchar
    }
}
