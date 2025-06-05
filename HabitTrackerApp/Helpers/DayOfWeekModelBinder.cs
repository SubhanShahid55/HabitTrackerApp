using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace HabitTrackerApp.Helpers;
public class DayOfWeekCollectionModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;
        if (string.IsNullOrEmpty(value))
        {
            bindingContext.Result = ModelBindingResult.Success(new List<DayOfWeek>());
            return Task.CompletedTask;
        }

        try
        {
            var dayValues = value.Split(',')
                                 .Select(int.Parse)
                                 .Select(x => (DayOfWeek)x)
                                 .ToList();

            bindingContext.Result = ModelBindingResult.Success(dayValues);
        }
        catch
        {
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}

public class DayOfWeekCollectionModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(ICollection<DayOfWeek>))
        {
            return new DayOfWeekCollectionModelBinder();
        }

        return null;
    }
}