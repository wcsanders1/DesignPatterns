using System.Data;
using System.IO;

namespace Adapter.Renderers
{
    public class DataRenderer
    {
        private readonly IDbDataAdapter dbDataAdapter;

        public DataRenderer(IDbDataAdapter dbDataAdapter)
        {
            this.dbDataAdapter = dbDataAdapter;
        }

        public void Render(TextWriter writer)
        {
            writer.WriteLine("Rendering Data:\n");
            var dataSet = new DataSet();

            dbDataAdapter.Fill(dataSet);

            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    writer.Write($"{column.ColumnName.PadRight(30)} ");
                }
                writer.WriteLine();

                foreach (DataRow row in table.Rows)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        writer.Write($"{row[i].ToString().PadRight(30)} ");
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}
