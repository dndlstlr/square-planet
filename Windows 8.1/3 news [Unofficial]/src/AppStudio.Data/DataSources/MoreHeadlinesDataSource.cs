using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class MoreHeadlinesDataSource : DataSourceBase<MenuSchema>
    {
        private readonly IEnumerable<MenuSchema> _data = new MenuSchema[]
		{
            new MenuSchema
            {
                Type = "Section",
                Title = "Sport",
                Icon = "/Assets/DataImages/7310310882_4e78f25688_o.png",
                Target = "SportPage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "Entertainment",
                Icon = "/Assets/DataImages/menuitem-icon.png",
                Target = "EntertainmentPage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "Political",
                Icon = "/Assets/DataImages/menuitem-icon.png",
                Target = "Political1Page",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "Science and Technology",
                Icon = "/Assets/DataImages/menuitem-icon.png",
                Target = "ScienceAndTechnologyPage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "Breaking News Videos",
                Icon = "/Assets/DataImages/menuitem-icon.png",
                Target = "BreakingNewsVideosPage",
            },
		};

        protected override string CacheKey
        {
            get { return "MoreHeadlinesDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return true; }
        }

        public async override Task<IEnumerable<MenuSchema>> LoadDataAsync()
        {
            return await Task.Run(() =>
            {
                return _data;
            });
        }
    }
}
