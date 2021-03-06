// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CM.BalancedScorecard.Web.DependencyResolution {
    using Data.Repository.Abstract;
    using Data.Repository.Abstract.Indicators;
    using Data.Repository.Implementation;
    using Data.Repository.Implementation.Indicators;
    using Domain.Abstract.Indicators;
    using Domain.Implementation.Indicators;
    using Domain.Model.Indicators;
    using Domain.Model.Users;
    using Resources.Abstract;
    using Resources.Implementation;
    using Services.Abstract;
    using Services.Abstract.Indicators;
    using Services.Implementation;
    using Services.Implementation.Indicators;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;

    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            For<IDbContext>().Use<BsContext>();
            For<IUnitOfWork>().Use<UnitOfWork>();
            For<IIndicatorsRepository>().Use<IndicatorsRepository>();
            For<IBaseRepository<IndicatorType>>().Use<BaseRepository<IndicatorType>>();
            For<IBaseRepository<User>>().Use<BaseRepository<User>>();
            For<IIndicatorsService>().Use<IndicatorsService>();
            For<IIndicatorStateCalculator>().Use<StateCalculator>();
            For<ITypeConfig>().Use<DataAnnotationsConfig>();
            For<IResourceFactory>().Use<ResourceFactory>();
            For<IIndicatorViewModelFactory>().Use<IndicatorViewModelFactory>();
        }

        #endregion
    }
}