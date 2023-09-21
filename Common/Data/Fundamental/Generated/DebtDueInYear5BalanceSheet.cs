/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2023 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/

using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using QuantConnect.Data.UniverseSelection;

namespace QuantConnect.Data.Fundamental
{
    /// <summary>
    /// Debt due within 5 year if the company provide maturity schedule in range e.g. 1-5 years, 2-5 years. Debt due under 5 years according to the debt maturity schedule reported by the company. If a range is reported by the company, the value will be collected under the maximum number of years (eg. 1-5 years, 3-5 years or 5 years will all be collected under this data point.)
    /// </summary>
    public class DebtDueInYear5BalanceSheet : MultiPeriodField
    {
        /// <summary>
        /// The default period
        /// </summary>
        protected override string DefaultPeriod => "TwelveMonths";

        /// <summary>
        /// Gets/sets the ThreeMonths period value for the field
        /// </summary>
        [JsonProperty("3M")]
        public double ThreeMonths => FundamentalService.Get<double>(Time, SecurityIdentifier, "FinancialStatements.BalanceSheet.DebtDueInYear5.ThreeMonths");

        /// <summary>
        /// Gets/sets the TwelveMonths period value for the field
        /// </summary>
        [JsonProperty("12M")]
        public double TwelveMonths => FundamentalService.Get<double>(Time, SecurityIdentifier, "FinancialStatements.BalanceSheet.DebtDueInYear5.TwelveMonths");

        /// <summary>
        /// Returns true if the field contains a value for the default period
        /// </summary>
        public override bool HasValue => FundamentalService.Get<double>(Time, SecurityIdentifier, "FinancialStatements.BalanceSheet.DebtDueInYear5.TwelveMonths") != NoValue;

        /// <summary>
        /// Returns the default value for the field
        /// </summary>
        public override double Value
        {
            get
            {
                var defaultValue = FundamentalService.Get<double>(Time, SecurityIdentifier, "FinancialStatements.BalanceSheet.DebtDueInYear5.TwelveMonths");
                if (defaultValue != NoValue)
                {
                    return defaultValue;
                }
                return base.Value;
            }
        }

        /// <summary>
        /// Gets a dictionary of period names and values for the field
        /// </summary>
        /// <returns>The dictionary of period names and values</returns>
        public override IReadOnlyDictionary<string, double> GetPeriodValues()
        {
            var result = new Dictionary<string, double>();
            foreach (var kvp in new[] { new Tuple<string, double>("3M",ThreeMonths), new Tuple<string, double>("12M",TwelveMonths) })
            {
                if(kvp.Item2 != NoValue)
                {
                    result[kvp.Item1] = kvp.Item2;
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the value of the field for the requested period
        /// </summary>
        /// <param name="period">The requested period</param>
        /// <returns>The value for the period</returns>
        public override double GetPeriodValue(string period) => FundamentalService.Get<double>(Time, SecurityIdentifier, $"FinancialStatements.BalanceSheet.DebtDueInYear5.{ConvertPeriod(period)}");

        /// <summary>
        /// Creates a new empty instance
        /// </summary>
        public DebtDueInYear5BalanceSheet()
        {
        }

        /// <summary>
        /// Creates a new instance for the given time and security
        /// </summary>
        public DebtDueInYear5BalanceSheet(DateTime time, SecurityIdentifier securityIdentifier) : base(time, securityIdentifier)
        {
        }
    }
}
