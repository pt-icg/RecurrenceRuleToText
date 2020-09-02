[![GitHub license](https://img.shields.io/github/license/pt-icg/RecurrenceRuleToText.svg)](https://github.com/pt-icg/RecurrenceRuleToText/blob/master/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/IcgSoftware.RecurrenceRuleToText.svg)](https://www.nuget.org/packages/IcgSoftware.RecurrenceRuleToText/)

# RecurrenceRuleToText

Human readable extension for [iCal.NET RecurrencePattern](https://github.com/rianjs/ical.net)

Thanks to [aditosoftware / rrule-parser](https://github.com/aditosoftware/rrule-parser)

Supported languages:

| Language   | Culture |
| ---------- | ------- |
| English    | en      |
| French     | fr      |
| German     | de      |
| Spanish    | es      |

### Usage example
```csharp
var recurrencePattern = new RecurrencePattern("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU");
recurrencePattern.ToText(); //every week on Monday, Tuesday

recurrencePattern = new RecurrencePattern("FREQ=YEARLY;BYDAY=WE;BYSETPOS=-1;BYMONTH=4");
recurrencePattern.ToText(); //every year on last Wednesday of April

var cultureInfo = new CultureInfo("de");
recurrencePattern.ToText(cultureInfo); //jedes Jahr am letzten Mittwoch im April

```
