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

using CM.BalancedScorecard.Data.Repository.Abstract.Indicators;
using CM.BalancedScorecard.Data.Repository.Implementation.Indicators;
using CM.BalancedScorecard.Domain.Model.Indicators;
using CM.BalancedScorecard.Domain.Model.Users;
using CM.BalancedScorecard.Services.Abstract.Indicators;

namespace CM.BalancedScorecard.Web.DependencyResolution {
    using Data.Repository.Implementation;
    using Data.Repository.Abstract;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using Services.Abstract;
    using Services.Implementation;
    using Domain.Abstract.Indicators;
    using Domain.Implementation.Indicators;
    using Services.Implementation.Indicators;
    using Resources.Abstract;
    using Resources.Implementation;

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