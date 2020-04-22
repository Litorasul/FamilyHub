namespace FamilyHub.Services.Data.Tests.AdministrationAreaTests
{
    using FamilyHub.Web.Areas.Administration.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class AdminRoutingTests
    {
        [Fact]
        public void DashboardShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/Dashboard")
                .To<DashboardController>(c => c.Index());
    }
}