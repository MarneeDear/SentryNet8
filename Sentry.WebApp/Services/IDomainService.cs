namespace Sentry.WebApp.Services
{
    public interface IDomainService
    {
        Sentry.Domain.Lynx.WebService.LynxWebServiceOperations LynxWebService { get; }
        Sentry.Domain.MDM.MasterDataWebServiceOperations MasterDataWebService { get; }
        Sentry.Domain.Marketo.MarketoServiceOperations MarketoService { get; }
        Sentry.Domain.Lynx.DataAccess.LynxDataOperations LynxDataOperations { get; }
        Sentry.Domain.AccountsPayable.AccountsPayableOperations AccountsPayableOperations { get; }
        Sentry.Domain.CentralizedAccessManagement.Operations CAMOperations { get; }  
        Sentry.Domain.PaperSave.PaperSaveOperations PaperSaveOperations { get; }
        Sentry.Domain.Forms.FormsOperations FormOperations { get; }
        Sentry.Domain.Users.UsersOperations UsersOperations { get; }
        Sentry.Domain.AccountsReceivable.AccountsReceivableOperations AccountsReceivableOperations { get; }
        Sentry.Domain.Security.SecurityOperations SecurityOperations { get; }
    }
}