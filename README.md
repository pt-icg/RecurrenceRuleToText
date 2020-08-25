# IcalNetHumanReadableExtension
Human readable extension for https://github.com/rianjs/ical.net

Supported languages:

| Language   | Culture |
| ---------- | ------- |
| English    | en      |
| German     | de      |

### Usage example
```csharp
var recurrencePattern = new RecurrencePattern("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU");
recurrencePattern.ToText(); //every week on Monday, Tuesday

recurrencePattern = new RecurrencePattern("FREQ=YEARLY;BYDAY=WE;BYSETPOS=-1;BYMONTH=4");
recurrencePattern.ToText(); //every year on last Wednesday of April

var cultureInfo = new CultureInfo("de");
recurrencePattern.ToText(cultureInfo); //jedes Jahr am letzten Mittwoch im April

```
 
Thanks to [casaucao / OrdinalNumbers](https://github.com/casaucao/OrdinalNumbers).