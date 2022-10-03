https://docs.microsoft.com/en-us/ef/core/get-started/wpf
https://docs.microsoft.com/en-us/windows/uwp/data-binding/data-binding-in-depth
https://docs.microsoft.com/en-us/ef/core/providers/sqlite/functions
https://docs.microsoft.com/en-ie/windows/apps/design/controls/navigationview#examples
https://www.learnentityframeworkcore.com/conventions#foreign-key
https://www.learnentityframeworkcore.com/configuration/fluent-api/hasforeignkey-method

https://github.com/Microsoft/InventorySample

https://xamlbrewer.wordpress.com/   <--
https://github.com/nickrandolph/old-wpf-blog/tree/master/12-DataBoundDialogBox

https://community.devexpress.com/blogs/wpf/archive/2022/02/07/dependency-injection-in-a-wpf-mvvm-application.aspx
			
VALIDATION
https://docs.microsoft.com/en-us/windows/communitytoolkit/mvvm/ObservableValidator

adding a view
https://khalidabuhakmeh.com/how-to-add-a-view-to-an-entity-framework-core-dbcontext

CREATE VIEW TransactionsWithCustomerDetails as
select t.TransactionDate, t.Type, t.Value, c.CustomerCode, c.Name
from Transactions t left join Customers c on t.CustomerId = c.CustomerId

BINDING
http://www.blackwasp.co.uk/WPFDataContext_2.aspx
http://www.blackwasp.co.uk/WPFValueConverter.aspx

DI
https://docs.microsoft.com/en-us/windows/communitytoolkit/mvvm/ioc

Restarting migrations
https://stackoverflow.com/questions/11679385/reset-entity-framework-migrations

FontIcons etc
https://docs.microsoft.com/en-us/windows/apps/design/style/segoe-ui-symbol-font