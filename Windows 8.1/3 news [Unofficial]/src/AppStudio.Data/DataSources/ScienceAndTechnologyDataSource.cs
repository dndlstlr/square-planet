using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class ScienceAndTechnologyDataSource : DataSourceBase<RssSchema>
    {
        private const string _url =@"http://feeds2.feedburner.com/co/Jjym";

        protected override string CacheKey
        {
            get { return "ScienceAndTechnologyDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<RssSchema>> LoadDataAsync()
        {
            try
            {
                var rssDataProvider = new RssDataProvider(_url);
                return await rssDataProvider.Load();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("ScienceAndTechnologyDataSourceDataSource.LoadData", ex.ToString());
                return new RssSchema[0];
            }
        }
    }
}
