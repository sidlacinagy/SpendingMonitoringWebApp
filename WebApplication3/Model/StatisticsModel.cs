using System.Text.Json;

namespace WebApplication3.Model
{
    public class StatisticsModel
    {
        public StatisticsModel(Object key, int sum, int count)
        {
            this.key=key;
            this.sum= sum;  
            this.count= count;
        }
        public Object key { get; set; }

        public int sum { get; set; }
        public int count { get; set; }
    }
}
