ko.extenders.dateArray = function (target) {
    //https://gist.github.com/cascadian/5c0ec685a7533b3b8ec6
    return ko.computed({
        read: function () {
            var value = target();
            if ($.isArray(value)) {
                const optionsWeekDay = { weekday: 'long' };
                const optionsDate = { year: 'numeric', month: '2-digit', day: '2-digit' };
                const optionsTime = { hour: '2-digit', minute: '2-digit' };
                for (let i = 0; i < value.length; i++) {
                    let d = new Date(value[i]);
                    value[i] = [d.toLocaleString(undefined, optionsWeekDay), d.toLocaleString(undefined, optionsDate), d.toLocaleString(undefined, optionsTime)];
                }
            }
            return value;
        },
        write: target
    });
};
const days = 31;
//public enum FrequencyType {
//    None = 0,
//    Secondly = 1,
//    Minutely = 2,
//    Hourly = 3,
//    Daily = 4,
//    Weekly = 5,
//    Monthly = 6,
//    Yearly = 7
//}
const frequencyEnumBase = 4;
const intMinValue = -2147483648; //Int32 MinValue = -2147483648 => default in RecurrencePattern.Count
const weekDayArray = ['MO', 'TU', 'WE', 'TH', 'FR', 'SA', 'SU'];
class HtmlTagItem {
    constructor(value, text, checked = false) {
        this.value = value;
        this.text = text;
        this.checked = checked;
    }
}
class RruleViewModel {
    constructor() {
        var thisObject = this;
        this.rRrule = ko.observable("");
        this.fromRruleString = ko.observable("FREQ=DAILY");
        this.reccuringEvent = ko.observable("no");
        const today = new Date();
        //var dateTimeString = today.getFullYear() + '-' + ('0' + (today.getMonth() + 1)).slice(-2) + '-' + ('0' + today.getDate()).slice(-2) + 'T18:00:00';
        var dateTimeString = today.getFullYear() + '-' + ('0' + (today.getMonth() + 1)).slice(-2) + '-' + ('0' + today.getDate()).slice(-2) + 'T' + ('0' + today.getHours()).slice(-2) + ':' + ('0' + today.getMinutes()).slice(-2) + ':00';
        this.starttime = ko.observable(dateTimeString);
        this.frequencyies = ko.observableArray([new HtmlTagItem('daily', 'Daily'), new HtmlTagItem('weekly', 'Weekly'), new HtmlTagItem('monthly', 'Monthly'), new HtmlTagItem('yearly', 'Yearly')]);
        this.selectedFrequency = ko.observable(this.frequencyies()[0]);
        //WEEKLY
        this.weekDays = ko.observableArray([
            new HtmlTagItem('MO', 'Mon', false),
            new HtmlTagItem('TU', 'Tue', false),
            new HtmlTagItem('WE', 'Wed', false),
            new HtmlTagItem('TH', 'Thu', false),
            new HtmlTagItem('FR', 'Fri', false),
            new HtmlTagItem('SA', 'Sat', false),
            new HtmlTagItem('SU', 'Sun', false),
        ]);
        this.toggleWeekDays = function (htmlTagItem) {
            thisObject.toggleButtonItem(htmlTagItem, thisObject.weekDays);
        };
        //MONTHLY
        this.monthlyOptions = ko.observable("monthly-days");
        //MONTHLY on
        let monthDayArray = new Array(days);
        for (let i = days; i--;)
            monthDayArray[i] = new HtmlTagItem((i + 1).toString(), (i + 1).toString(), false);
        this.monthDays = ko.observableArray(monthDayArray);
        this.toggleMonthDays = function (htmlTagItem) {
            thisObject.toggleButtonItem(htmlTagItem, thisObject.monthDays);
        };
        //MONTHLY on the
        this.monthByDayPos = ko.observableArray([
            new HtmlTagItem((1).toString(), 'First'),
            new HtmlTagItem((2).toString(), 'Second'),
            new HtmlTagItem((3).toString(), 'Third'),
            new HtmlTagItem((4).toString(), 'Fourth'),
            new HtmlTagItem((-1).toString(), 'Last')
        ]);
        this.selectedMonthByDayPos = ko.observable(this.monthByDayPos()[0]);
        this.monthByDayPosName = ko.observableArray([
            new HtmlTagItem('MO', 'Monday'),
            new HtmlTagItem('TU', 'Tuesday'),
            new HtmlTagItem('WE', 'Wednesday'),
            new HtmlTagItem('TH', 'Thursday'),
            new HtmlTagItem('FR', 'Friday'),
            new HtmlTagItem('SA', 'Saturday'),
            new HtmlTagItem('SU', 'Sunday')
        ]);
        this.selectedMonthByDayPosName = ko.observable(this.monthByDayPosName()[0]);
        //YEARLY
        this.yearlyOptions = ko.observable("yearly-one-month");
        //YEARLY One Month Out of the Year
        this.yearlyByMonth = ko.observableArray([
            new HtmlTagItem((1).toString(), 'January'),
            new HtmlTagItem((2).toString(), 'February'),
            new HtmlTagItem((3).toString(), 'March'),
            new HtmlTagItem((4).toString(), 'April'),
            new HtmlTagItem((5).toString(), 'May'),
            new HtmlTagItem((6).toString(), 'June'),
            new HtmlTagItem((7).toString(), 'July'),
            new HtmlTagItem((8).toString(), 'August'),
            new HtmlTagItem((9).toString(), 'September'),
            new HtmlTagItem((10).toString(), 'October'),
            new HtmlTagItem((11).toString(), 'November'),
            new HtmlTagItem((12).toString(), 'December')
        ]);
        this.selectedYearlyByMonth = ko.observable(this.yearlyByMonth()[0]);
        let yearlyByMonthDayArray = new Array(days);
        for (let i = days; i--;)
            yearlyByMonthDayArray[i] = new HtmlTagItem((i + 1).toString(), (i + 1).toString(), false);
        this.yearlyByMonthDay = ko.observableArray(yearlyByMonthDayArray);
        this.selectedYearlyByMonthDay = ko.observable(this.yearlyByMonthDay()[0]);
        //YEARLY Multiple Months
        this.yearlyMultipleMonths = ko.observableArray([
            new HtmlTagItem((1).toString(), 'Jan', false),
            new HtmlTagItem((2).toString(), 'Feb', false),
            new HtmlTagItem((3).toString(), 'Mar', false),
            new HtmlTagItem((4).toString(), 'Apr', false),
            new HtmlTagItem((5).toString(), 'May', false),
            new HtmlTagItem((6).toString(), 'Jun', false),
            new HtmlTagItem((7).toString(), 'Jul', false),
            new HtmlTagItem((8).toString(), 'Aug', false),
            new HtmlTagItem((9).toString(), 'Sep', false),
            new HtmlTagItem((10).toString(), 'Oct', false),
            new HtmlTagItem((11).toString(), 'Nov', false),
            new HtmlTagItem((12).toString(), 'Dec', false)
        ]);
        this.toggleYearlyMultipleMonths = function (HtmlTagItem) {
            thisObject.toggleButtonItem(HtmlTagItem, thisObject.yearlyMultipleMonths);
        };
        //YEARLY precise
        this.yearlyBySetPos = ko.observableArray([
            new HtmlTagItem((1).toString(), 'First'),
            new HtmlTagItem((2).toString(), 'Second'),
            new HtmlTagItem((3).toString(), 'Third'),
            new HtmlTagItem((4).toString(), 'Fourth'),
            new HtmlTagItem((-1).toString(), 'Last')
        ]);
        this.selectedYearlyBySetPos = ko.observable(this.yearlyBySetPos()[0]);
        this.yearlyByDay = ko.observableArray([
            new HtmlTagItem('MO', 'Monday'),
            new HtmlTagItem('TU', 'Tuesday'),
            new HtmlTagItem('WE', 'Wednesday'),
            new HtmlTagItem('TH', 'Thursday'),
            new HtmlTagItem('FR', 'Friday'),
            new HtmlTagItem('SA', 'Saturday'),
            new HtmlTagItem('SU', 'Sunday')
        ]);
        this.selectedYearlyByDay = ko.observable(this.yearlyByDay()[0]);
        this.yearlyByMonthWithBySetPosByDay = ko.observableArray([
            new HtmlTagItem((1).toString(), 'January'),
            new HtmlTagItem((2).toString(), 'February'),
            new HtmlTagItem((3).toString(), 'March'),
            new HtmlTagItem((4).toString(), 'April'),
            new HtmlTagItem((5).toString(), 'May'),
            new HtmlTagItem((6).toString(), 'June'),
            new HtmlTagItem((7).toString(), 'July'),
            new HtmlTagItem((8).toString(), 'August'),
            new HtmlTagItem((9).toString(), 'September'),
            new HtmlTagItem((10).toString(), 'October'),
            new HtmlTagItem((11).toString(), 'November'),
            new HtmlTagItem((12).toString(), 'December')
        ]);
        this.selectedYearlyByMonthWithBySetPosByDay = ko.observable(this.yearlyByMonthWithBySetPosByDay()[0]);
        //everyrule-interval
        this.everyRuleInterval = ko.observable(1);
        //end rules
        this.endRule = ko.observableArray([
            new HtmlTagItem('never', 'Never'),
            new HtmlTagItem('occurrences', 'Occurrences'),
            new HtmlTagItem('until', 'Until')
        ]);
        this.selectedEndRule = ko.observable(this.endRule()[0]);
        this.endRuleOccurrences = ko.observable(1);
        var dateString = today.getFullYear() + '-' + ('0' + (today.getMonth() + 1)).slice(-2) + '-' + ('0' + today.getDate()).slice(-2);
        this.endRuleUntil = ko.observable(dateString);
        //rrule-result rruletext rrulecode
        this.rruleCode = ko.observable('rruleCode');
        this.rruleText = ko.observable('rruleText');
        this.rruleOutput = ko.observableArray().extend({ dateArray: true });
        //rruleerror, rrulehint
        this.rruleError = ko.observable('rruleError');
        this.rruleHint = ko.observable('rruleHint');
        //subscribe
        this.starttime.subscribe((newValue) => { this.getRule(newValue); });
        //this.reccuringEvent.subscribe((newValue) => { this.getRule(newValue); });
        //this.reccuringEvent.subscribe(thisObject.getRule);
        this.selectedFrequency.subscribe((newValue) => { this.getRule(newValue); });
        this.weekDays.subscribe((newValue) => { this.getRule(newValue); });
        this.monthlyOptions.subscribe((newValue) => { this.getRule(newValue); });
        this.monthDays.subscribe((newValue) => { this.getRule(newValue); });
        this.selectedMonthByDayPos.subscribe((newValue) => { this.getRule(newValue); });
        this.selectedMonthByDayPosName.subscribe((newValue) => { this.getRule(newValue); });
        this.yearlyOptions.subscribe((newValue) => { this.getRule(newValue); });
        this.selectedYearlyByMonth.subscribe((newValue) => { this.getRule(newValue); });
        this.selectedYearlyByMonthDay.subscribe((newValue) => { this.getRule(newValue); });
        this.yearlyMultipleMonths.subscribe((newValue) => { this.getRule(newValue); });
        this.selectedYearlyBySetPos.subscribe((newValue) => { this.getRule(newValue); });
        this.selectedYearlyByDay.subscribe((newValue) => { this.getRule(newValue); });
        this.selectedYearlyByMonthWithBySetPosByDay.subscribe((newValue) => { this.getRule(newValue); });
        this.everyRuleInterval.subscribe((newValue) => { this.getRule(newValue); });
        this.selectedEndRule.subscribe((newValue) => { this.getRule(newValue); });
        this.endRuleOccurrences.subscribe((newValue) => { this.getRule(newValue); });
        this.endRuleUntil.subscribe((newValue) => { this.getRule(newValue); });
    }
    getRule(newValue) {
        let startdate = this.starttime();
        let frequency = this.selectedFrequency().value;
        let interval = this.everyRuleInterval();
        let count = intMinValue;
        let until = null;
        let byday = [];
        let bymonthday = [];
        let bysetposition = [];
        let bymonth = [];
        //let antiForgeryToken = $('input[name="CSRF-TOKEN-MOONGLADE-FORM"]').val()
        let antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
        switch (frequency) {
            case "weekly":
                byday = this.weekDays().filter(i => i.checked).map(i => i.value);
                break;
            case "monthly":
                let monthlyOption = this.monthlyOptions();
                switch (monthlyOption) {
                    case "monthly-days":
                        bymonthday = this.monthDays().filter(i => i.checked).map(i => i.value);
                        break;
                    case "monthly-precise":
                        bysetposition.push(this.selectedMonthByDayPos().value);
                        byday.push(this.selectedMonthByDayPosName().value);
                        break;
                }
                break;
            case "yearly":
                let yearlyOption = this.yearlyOptions();
                switch (yearlyOption) {
                    case "yearly-one-month":
                        bymonth.push(this.selectedYearlyByMonth().value);
                        bymonthday.push(this.selectedYearlyByMonthDay().value);
                        break;
                    case "yearly-multiple-months":
                        bymonth = this.yearlyMultipleMonths().filter(i => i.checked).map(i => i.value);
                        break;
                    case "yearly-precise":
                        bysetposition.push(this.selectedYearlyBySetPos().value);
                        byday.push(this.selectedYearlyByDay().value);
                        bymonth.push(this.selectedYearlyByMonthWithBySetPosByDay().value);
                        break;
                }
                break;
        }
        let endRuleSelected = this.selectedEndRule().value;
        switch (endRuleSelected) {
            case "never":
                break;
            case "occurrences":
                count = this.endRuleOccurrences();
                break;
            case "until":
                until = this.endRuleUntil(); //erstmal nur für Format 2020-12-31
                break;
        }
        let rRuleWrapper = { "StartDate": startdate, "Frequency": frequency, "Interval": Number(interval), "ByDayValue": byday, "ByMonth": bymonth, "ByMonthDay": bymonthday, "BySetPosition": bysetposition, "Count": count, "Until": until };
        //https://www.thereformedprogrammer.net/asp-net-core-razor-pages-how-to-implement-ajax-requests/
        $.ajax({
            type: 'POST',
            url: '/Home/CreateRRule',
            contentType: 'application/json; charset=utf-8',
            //headers: {
            //    "CSRF-TOKEN-MOONGLADE-FORM": $('input[name="CSRF-TOKEN-MOONGLADE-FORM"]').val()
            //},            
            //headers = {
            //    "__RequestVerificationToken": antiForgeryToken
            //},            
            //headers = '"__RequestVerificationToken": antiForgeryToken,'
            //headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            headers: { "RequestVerificationToken": `${antiForgeryToken}` },
            //data: {
            //    __RequestVerificationToken: antiForgeryToken,
            //    rRuleWrapper: rRuleWrapper
            //},
            //dataType: 'JSON',            
            data: JSON.stringify(rRuleWrapper),
            success: (result) => {
                this.rruleCode(result.recurrencePatternString);
                this.rruleText(result.recurrencePatternText);
                this.rruleOutput(result.recurrencePatternList);
                this.rruleError(result.errorText);
                this.rruleHint(result.hintText);
                this.reccuringEvent('yes');
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => {
                this.rruleError(`${XMLHttpRequest.status} ${textStatus}`);
                this.reccuringEvent('yes');
            }
        });
    }
    ;
    toggleButtonItem(item, observableArray) {
        let index = observableArray.indexOf(item);
        if (index !== -1) {
            let currentItem = observableArray()[index];
            let newItem = new HtmlTagItem(currentItem.value, currentItem.text, !currentItem.checked);
            observableArray.replace(currentItem, newItem);
        }
    }
    refresh() {
        this.getRule(0);
        //this.reccuringEvent('yes')
    }
}
ko.options.deferUpdates = true;
var viewModel = new RruleViewModel();
ko.applyBindings(viewModel);
export { viewModel };
//# sourceMappingURL=rrule-viewmodel-ts.js.map