﻿using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MyAbpApp.Pages;

[Collection(MyAbpAppTestConsts.CollectionDefinitionName)]
public class Index_Tests : MyAbpAppWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
