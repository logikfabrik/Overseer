// <copyright file="AddConnectionViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.ViewModels
{
    using Caliburn.Micro;
    using Settings;

    /// <summary>
    /// The <see cref="AddConnectionViewModel" /> class.
    /// </summary>
    /// <seealso cref="WPF.ViewModels.AddConnectionViewModel" />
    public class AddConnectionViewModel : WPF.ViewModels.AddConnectionViewModel
    {
        private string _url = "https://";
        private string _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConnectionViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="buildProviderSettingsRepository">The build provider settings repository.</param>
        public AddConnectionViewModel(IEventAggregator eventAggregator, IBuildProviderSettingsRepository buildProviderSettingsRepository)
            : base(eventAggregator, buildProviderSettingsRepository)
        {
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
                NotifyOfPropertyChange(() => Url);
            }
        }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token
        {
            get
            {
                return _token;
            }

            set
            {
                _token = value;
                NotifyOfPropertyChange(() => Token);
            }
        }

        protected override BuildProviderSettings GetSettings()
        {
            return new BuildProviderSettings
            {
                Name = Name,
                BuildProviderTypeName = typeof(BuildProvider).AssemblyQualifiedName,
                Settings = new[]
                {
                    new Setting {Name = "Url", Value = Url},
                    new Setting {Name = "Token", Value = Token}
                }
            };
        }
    }
}
