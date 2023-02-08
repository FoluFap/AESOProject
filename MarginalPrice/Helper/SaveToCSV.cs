using System.ComponentModel;

namespace MarginalPrice.Helper
{
    public class SaveToCSV
    {
        //Generic method to generate list of any object 
        public  bool  SaveToCsv<T>(List<T> maginalPriceReportData, string path)
        {
            bool resp = false;
            try
            {
                var lines = new List<string>();
                IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
                var header = string.Join(",", props.ToList().Select(x => x.Name));
                lines.Add(header);
                var valueLines = maginalPriceReportData.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
                lines.AddRange(valueLines);
                File.WriteAllLines(path, lines.ToArray());

                resp = true;
            }
            catch (Exception ex)
            {
                //log the error encountered
                
            }

            return resp;
         
        }
    }
}
