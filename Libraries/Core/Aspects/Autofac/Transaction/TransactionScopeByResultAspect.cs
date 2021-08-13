using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    /// <summary>
    /// IResult/Task<IResult> dönen methotlarda Success durumunu kontrol edip Transaction işlemini tamamlar.
    /// IResult.Success false dönerse tüm yapılan işlemleri geri alır.
    /// </summary>
    /// 
    public class TransactionScopeByResultAspect : MethodInterception
    {
        public async override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    var returnType = invocation.ReturnValue;
                    var type = invocation.ReturnValue.GetType();
                    if (returnType is IResult result)
                    {
                        if (result.Success)
                            transactionScope.Complete();
                        else
                            transactionScope.Dispose();
                    }
                    else if (returnType is Task<IResult> taskedResult)
                    {
                        var response = await taskedResult;
                        if (response.Success)
                            transactionScope.Complete();
                        else
                            transactionScope.Dispose();
                    }
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();
                }
            }
        }
    }
}