export class HtmlTagItem {
    constructor(value, text, checked = false) {
        this.value = value;
        this.text = text;
        this.checked = checked;
    }
}
const days = 31;
export class RRuleViewModel {
    constructor() {
        //public rRrule: Observable<string>
        //public fromRruleString: Observable<string>
        //public reccuringEvent: Observable<string>
        //public ShowY: boolean
        //public ShowN: boolean
        //public IfY: boolean
        this.ShowAfterBoot = false;
        this.ShowReccuringEvent = false;
        var thisObject = this;
        //this.rRrule = ko.observable("")
        //this.fromRruleString = ko.observable("FREQ=DAILY")
        //this.reccuringEvent = ko.observable("no")
        this.ShowReccuringEvent = true;
        //this.ShowY = true
        //this.ShowN = false
        //this.IfY = true
        //this.IfN = false
        const today = new Date();
        var dateTimeString = today.getFullYear() + '-' + ('0' + (today.getMonth() + 1)).slice(-2) + '-' + ('0' + today.getDate()).slice(-2) + 'T' + ('0' + today.getHours()).slice(-2) + ':' + ('0' + today.getMinutes()).slice(-2) + ':00';
        this.Starttime = dateTimeString;
        this.Frequencyies = [new HtmlTagItem('daily', 'Daily'), new HtmlTagItem('weekly', 'Weekly'), new HtmlTagItem('monthly', 'Monthly'), new HtmlTagItem('yearly', 'Yearly')];
        this.SelectedFrequency = this.Frequencyies[0].value;
        //WEEKLY
        this.WeekDays = [
            new HtmlTagItem('MO', 'Mon', false),
            new HtmlTagItem('TU', 'Tue', false),
            new HtmlTagItem('WE', 'Wed', false),
            new HtmlTagItem('TH', 'Thu', false),
            new HtmlTagItem('FR', 'Fri', false),
            new HtmlTagItem('SA', 'Sat', false),
            new HtmlTagItem('SU', 'Sun', false),
        ];
        this.ToggleWeekDays = function (htmlTagItem) {
            htmlTagItem.checked = !htmlTagItem.checked;
            //thisObject.ToggleButtonItem(htmlTagItem, thisObject.WeekDays);
        };
        ////MONTHLY
        this.MonthlyOptions = "monthly-days";
        ////MONTHLY on
        this.MonthDays = new Array(days);
        for (let i = days; i--;)
            this.MonthDays[i] = new HtmlTagItem((i + 1).toString(), (i + 1).toString(), false);
        //MONTHLY on the
        this.MonthByDayPos = [
            new HtmlTagItem((1).toString(), 'First'),
            new HtmlTagItem((2).toString(), 'Second'),
            new HtmlTagItem((3).toString(), 'Third'),
            new HtmlTagItem((4).toString(), 'Fourth'),
            new HtmlTagItem((-1).toString(), 'Last')
        ];
        this.SelectedMonthByDayPos = this.MonthByDayPos[0].value;
        this.MonthByDayPosName = [
            new HtmlTagItem('MO', 'Monday'),
            new HtmlTagItem('TU', 'Tuesday'),
            new HtmlTagItem('WE', 'Wednesday'),
            new HtmlTagItem('TH', 'Thursday'),
            new HtmlTagItem('FR', 'Friday'),
            new HtmlTagItem('SA', 'Saturday'),
            new HtmlTagItem('SU', 'Sunday')
        ];
        this.SelectedMonthByDayPosName = this.MonthByDayPosName[0].value;
        //YEARLY
        this.YearlyOptions = "yearly-one-month";
        //YEARLY One Month Out of the Year
        this.YearlyByMonth = [
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
        ];
        this.SelectedYearlyByMonth = this.YearlyByMonth[0].value;
        this.YearlyByMonthDay = new Array(days);
        for (let i = days; i--;)
            this.YearlyByMonthDay[i] = new HtmlTagItem((i + 1).toString(), (i + 1).toString(), false);
        this.SelectedYearlyByMonthDay = this.YearlyByMonthDay[0].value;
        //YEARLY Multiple Months
        this.YearlyMultipleMonths = [
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
        ];
        //YEARLY precise
        this.YearlyBySetPos = [
            new HtmlTagItem((1).toString(), 'First'),
            new HtmlTagItem((2).toString(), 'Second'),
            new HtmlTagItem((3).toString(), 'Third'),
            new HtmlTagItem((4).toString(), 'Fourth'),
            new HtmlTagItem((-1).toString(), 'Last')
        ];
        this.SelectedYearlyBySetPos = this.YearlyBySetPos[0].value;
        this.YearlyByDay = [
            new HtmlTagItem('MO', 'Monday'),
            new HtmlTagItem('TU', 'Tuesday'),
            new HtmlTagItem('WE', 'Wednesday'),
            new HtmlTagItem('TH', 'Thursday'),
            new HtmlTagItem('FR', 'Friday'),
            new HtmlTagItem('SA', 'Saturday'),
            new HtmlTagItem('SU', 'Sunday')
        ];
        this.SelectedYearlyByDay = this.YearlyByDay[0].value;
        this.YearlyByMonthWithBySetPosByDay = [
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
        ];
        this.SelectedYearlyByMonthWithBySetPosByDay = this.YearlyByMonthWithBySetPosByDay[0].value;
        //everyrule-interval
        this.EveryRuleInterval = 1;
        //end rules
        this.EndRules = [
            new HtmlTagItem('never', 'Never'),
            new HtmlTagItem('occurrences', 'Occurrences'),
            new HtmlTagItem('until', 'Until')
        ];
        this.SelectedEndRule = this.EndRules[0].value;
        this.EndRuleOccurrences = 1;
        var dateString = today.getFullYear() + '-' + ('0' + (today.getMonth() + 1)).slice(-2) + '-' + ('0' + today.getDate()).slice(-2);
        this.EndRuleUntil = dateString;
        ////rrule-result rruletext rrulecode
        this.RRuleCode = "";
        this.RRuleText = "";
        this.RRuleOutput = new Array(0);
        ////rruleerror, rrulehint
        this.RRuleError = "";
        this.RRuleHint = "";
    }
}
//export default RRuleViewModel 
//# sourceMappingURL=RRuleViewModel.js.map