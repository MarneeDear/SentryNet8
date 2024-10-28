using Microsoft.Extensions.Options;
using System;

namespace Sentry.WebApp.Services
{
    public class DomainService : IDomainService
    {        
        private readonly Config _config;

        public DomainService(IOptions<Config> config)
        {
            _config = config.Value;
        }

        private Sentry.Domain.Lynx.WebService.LynxWebServiceOperations _lynx = null;

        public Sentry.Domain.Lynx.WebService.LynxWebServiceOperations LynxWebService
        {
            get
            {
                if (_lynx == null)
                {
                    _lynx = new Sentry.Domain.Lynx.WebService.LynxWebServiceOperations(_config.LynxWebService.Url, String.Empty);
                }

                return _lynx;
            }
        }

        private Sentry.Domain.MDM.MasterDataWebServiceOperations _mdm = null;

        public Sentry.Domain.MDM.MasterDataWebServiceOperations MasterDataWebService
        {
            get
            {
                if (_mdm == null)
                {
                    _mdm = new Sentry.Domain.MDM.MasterDataWebServiceOperations(_config.MasterDataWebService.Url, String.Empty);
                }

                return _mdm;
            }
        }

        private Sentry.Domain.Lynx.DataAccess.LynxDataOperations _lynxData = null;

        public Sentry.Domain.Lynx.DataAccess.LynxDataOperations LynxDataOperations
        {
            get
            {
                if (_lynxData == null)
                {
                    _lynxData = new Sentry.Domain.Lynx.DataAccess.LynxDataOperations(_config.ConnectionStrings.CrmDbContext);
                }

                return _lynxData;
            }
        }

        private Sentry.Domain.AccountsPayable.AccountsPayableOperations _apData = null;

        public Sentry.Domain.AccountsPayable.AccountsPayableOperations AccountsPayableOperations
        {
            get
            {
                if (_apData == null)
                {
                    _apData = new Sentry.Domain.AccountsPayable.AccountsPayableOperations(_config.UAFServices.BaseUrl, _config.UAFServices.APIKey);
                }

                return _apData;
            }
        }

        private Sentry.Domain.CentralizedAccessManagement.Operations _camData = null;

        public Sentry.Domain.CentralizedAccessManagement.Operations CAMOperations
        {
            get
            {
                if (_camData == null)
                {
                    _camData = new Sentry.Domain.CentralizedAccessManagement.Operations(_config.UAFServices.BaseUrl, _config.UAFServices.APIKey);
                }

                return _camData;
            }
        }

        private Sentry.Domain.PaperSave.PaperSaveOperations _paperSaveOperations = null;

        public Sentry.Domain.PaperSave.PaperSaveOperations PaperSaveOperations
        {
            get
            {
                if (_paperSaveOperations == null)
                {
                    _paperSaveOperations = new Sentry.Domain.PaperSave.PaperSaveOperations(_config.UAFServices.BaseUrl, _config.UAFServices.APIKey);
                }

                return _paperSaveOperations;
            }
        }

        private Sentry.Domain.Forms.FormsOperations _formOperations = null;

        public Sentry.Domain.Forms.FormsOperations FormOperations
        {
            get
            {
                if (_formOperations == null)
                {
                    _formOperations = new Sentry.Domain.Forms.FormsOperations(_config.UAFServices.BaseUrl, _config.UAFServices.APIKey);
                }

                return _formOperations;
            }
        }

        private Sentry.Domain.Users.UsersOperations _userOperations = null;

        public Sentry.Domain.Users.UsersOperations UsersOperations
        {
            get
            {
                if (_userOperations == null)
                {
                    _userOperations = new Sentry.Domain.Users.UsersOperations(_config.UAFServices.BaseUrl, _config.UAFServices.APIKey);
                }

                return _userOperations;
            }
        }

        private Sentry.Domain.AccountsReceivable.AccountsReceivableOperations _arData = null;

        public Sentry.Domain.AccountsReceivable.AccountsReceivableOperations AccountsReceivableOperations
        {
            get
            {
                if (_arData == null)
                {
                    _arData = new Sentry.Domain.AccountsReceivable.AccountsReceivableOperations(_config.UAFServices.BaseUrl, _config.UAFServices.APIKey);
                }

                return _arData;
            }
        }

        private Sentry.Domain.Marketo.MarketoServiceOperations _marketo = null;

        public Sentry.Domain.Marketo.MarketoServiceOperations MarketoService
        {
            get
            {
                if (_marketo == null)
                {
                    _marketo = new Sentry.Domain.Marketo.MarketoServiceOperations(_config.MarketoService.Url, String.Empty);
                }

                return _marketo;
            }
        }

        private Sentry.Domain.Security.SecurityOperations _security = null;

        public Sentry.Domain.Security.SecurityOperations SecurityOperations
        {
            get
            {
                if (_security == null)
                {
                    _security = new Sentry.Domain.Security.SecurityOperations(_config.UAFServices.BaseUrl, _config.UAFServices.APIKey);
                }

                return _security;
            }
        }
    }
}
