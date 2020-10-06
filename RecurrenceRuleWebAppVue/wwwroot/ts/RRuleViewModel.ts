export class HtmlTagItem {
    public value: string
    public text: string
    public checked: boolean

    constructor(value: string, text: string, checked: boolean = false) {
        this.value = value
        this.text = text
        this.checked = checked
    }
}

const days = 31;

export class RRuleViewModel {

    //public rRrule: Observable<string>
    //public fromRruleString: Observable<string>
    //public reccuringEvent: Observable<string>

    //public ShowY: boolean
    //public ShowN: boolean
    //public IfY: boolean
    public ShowAfterBoot: boolean = false
    public ShowReccuringEvent: boolean = false
    public Starttime: string
    public Frequencyies: Array<HtmlTagItem>
    public SelectedFrequency: string

    public WeekDays: Array<HtmlTagItem>
    public ToggleWeekDays: Function

    public MonthlyOptions: string
    public MonthDays: Array<HtmlTagItem>    
    public MonthByDayPos: Array<HtmlTagItem>
    public SelectedMonthByDayPos: string
    public MonthByDayPosName: Array<HtmlTagItem>
    public SelectedMonthByDayPosName: string

    public YearlyOptions: string
    public YearlyByMonth: Array<HtmlTagItem>
    public SelectedYearlyByMonth: string
    public YearlyByMonthDay: Array<HtmlTagItem>
    public SelectedYearlyByMonthDay: string
    public YearlyMultipleMonths: Array<HtmlTagItem>
    public YearlyBySetPos: Array<HtmlTagItem>
    public SelectedYearlyBySetPos: string
    public YearlyByDay: Array<HtmlTagItem>
    public SelectedYearlyByDay: string
    public YearlyByMonthWithBySetPosByDay: Array<HtmlTagItem>
    public SelectedYearlyByMonthWithBySetPosByDay: string

    public EveryRuleInterval: number

    public EndRules: Array<HtmlTagItem>
    public SelectedEndRule: string
    public EndRuleOccurrences: number
    public EndRuleUntil: string

    public RRuleCode: string
    public RRuleText: string
    public RRuleOutput: Array<Array<string>>

    public RRuleError: string
    public RRuleHint: string

    constructor() {

        var thisObject = this
        //this.rRrule = ko.observable("")
        //this.fromRruleString = ko.observable("FREQ=DAILY")
        //this.reccuringEvent = ko.observable("no")

        this.ShowReccuringEvent = true
        //this.ShowY = true
        //this.ShowN = false
        //this.IfY = true
        //this.IfN = false

        const today = new Date();
        var dateTimeString = today.getFullYear() + '-' + ('0' + (today.getMonth() + 1)).slice(-2) + '-' + ('0' + today.getDate()).slice(-2) + 'T' + ('0' + today.getHours()).slice(-2) + ':' + ('0' + today.getMinutes()).slice(-2) + ':00';
        this.Starttime = dateTimeString;

        this.Frequencyies = [new HtmlTagItem('daily', 'Daily'), new HtmlTagItem('weekly', 'Weekly'), new HtmlTagItem('monthly', 'Monthly'), new HtmlTagItem('yearly', 'Yearly')]
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

        this.ToggleWeekDays = function (htmlTagItem: HtmlTagItem) {
            htmlTagItem.checked = !htmlTagItem.checked
            //thisObject.ToggleButtonItem(htmlTagItem, thisObject.WeekDays);
        }

        ////MONTHLY
        this.MonthlyOptions = "monthly-days"

        ////MONTHLY on
        this.MonthDays = new Array<HtmlTagItem>(days);
        for (let i = days; i--;) this.MonthDays[i] = new HtmlTagItem((i + 1).toString(), (i + 1).toString(), false);
        
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

        this.YearlyByMonthDay = new Array<HtmlTagItem>(days)
        for (let i = days; i--;) this.YearlyByMonthDay[i] = new HtmlTagItem((i + 1).toString(), (i + 1).toString(), false);
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
        ]
        this.SelectedYearlyBySetPos = this.YearlyBySetPos[0].value

        this.YearlyByDay = [
            new HtmlTagItem('MO', 'Monday'),
            new HtmlTagItem('TU', 'Tuesday'),
            new HtmlTagItem('WE', 'Wednesday'),
            new HtmlTagItem('TH', 'Thursday'),
            new HtmlTagItem('FR', 'Friday'),
            new HtmlTagItem('SA', 'Saturday'),
            new HtmlTagItem('SU', 'Sunday')
        ]
        this.SelectedYearlyByDay = this.YearlyByDay[0].value

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
        ]
        this.SelectedYearlyByMonthWithBySetPosByDay = this.YearlyByMonthWithBySetPosByDay[0].value


        //everyrule-interval
        this.EveryRuleInterval = 1

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
        this.RRuleCode = ""
        this.RRuleText = ""
        this.RRuleOutput = new Array<Array<string>>(0)

        ////rruleerror, rrulehint
        this.RRuleError = ""
        this.RRuleHint = ""

    }

    //private getRule(newValue: string | number | HtmlTagItem | HtmlTagItem[]) {
    //    let startdate = this.starttime()
    //    let frequency = this.selectedFrequency().value;
    //    let interval = this.everyRuleInterval()
    //    let count = intMinValue;
    //    let until = null;
    //    let byday: string[] = [];
    //    let bymonthday: string[] = [];
    //    let bysetposition = [];
    //    let bymonth: string[] = [];
    //    //let antiForgeryToken = $('input[name="CSRF-TOKEN-MOONGLADE-FORM"]').val()
    //    let antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();

    //    switch (frequency) {
    //        case "weekly":
    //            byday = this.weekDays().filter(i => i.checked).map(i => i.value);
    //            break;
    //        case "monthly":
    //            let monthlyOption = this.monthlyOptions();
    //            switch (monthlyOption) {
    //                case "monthly-days":
    //                    bymonthday = this.monthDays().filter(i => i.checked).map(i => i.value);
    //                    break;
    //                case "monthly-precise":
    //                    bysetposition.push(this.selectedMonthByDayPos().value);
    //                    byday.push(this.selectedMonthByDayPosName().value);
    //                    break;
    //            }
    //            break;
    //        case "yearly":
    //            let yearlyOption = this.yearlyOptions();
    //            switch (yearlyOption) {
    //                case "yearly-one-month":
    //                    bymonth.push(this.selectedYearlyByMonth().value);
    //                    bymonthday.push(this.selectedYearlyByMonthDay().value);
    //                    break;
    //                case "yearly-multiple-months":
    //                    bymonth = this.yearlyMultipleMonths().filter(i => i.checked).map(i => i.value);
    //                    break;
    //                case "yearly-precise":
    //                    bysetposition.push(this.selectedYearlyBySetPos().value);
    //                    byday.push(this.selectedYearlyByDay().value);
    //                    bymonth.push(this.selectedYearlyByMonthWithBySetPosByDay().value);
    //                    break;
    //            }
    //            break;
    //    }

    //    let endRuleSelected = this.selectedEndRule().value;
    //    switch (endRuleSelected) {
    //        case "never":
    //            break;
    //        case "occurrences":
    //            count = this.endRuleOccurrences();
    //            break;
    //        case "until":
    //            until = this.endRuleUntil(); //erstmal nur für Format 2020-12-31
    //            break;
    //    }
    //    let rRuleWrapper = { "StartDate": startdate, "Frequency": frequency, "Interval": Number(interval), "ByDayValue": byday, "ByMonth": bymonth, "ByMonthDay": bymonthday, "BySetPosition": bysetposition, "Count": count, "Until": until };
    //    //https://www.thereformedprogrammer.net/asp-net-core-razor-pages-how-to-implement-ajax-requests/

    //    //axios-test
    //    //$.ajax({
    //    //    type: 'POST',
    //    //    url: '/Home/CreateRRule',
    //    //    contentType: 'application/json; charset=utf-8',
    //    //    headers: { "RequestVerificationToken": `${antiForgeryToken}` },
    //    //    data: JSON.stringify(rRuleWrapper),
    //    //    success: (result) => {
    //    //        this.rruleCode(result.recurrencePatternString)
    //    //        this.rruleText(result.recurrencePatternText)
    //    //        this.rruleOutput(result.recurrencePatternList)
    //    //        this.rruleError(result.errorText);
    //    //        this.rruleHint(result.hintText);
    //    //        this.reccuringEvent('yes')
    //    //    },
    //    //    error: (XMLHttpRequest, textStatus, errorThrown) => {
    //    //        this.rruleError(`${XMLHttpRequest.status} ${textStatus}`);
    //    //        this.reccuringEvent('yes')
    //    //    }
    //    //});

    //};

    //private ToggleButtonItem(item: HtmlTagItem, array: Array<HtmlTagItem>) {
    //    let index = array.indexOf(item);
    //    if (index !== -1) {
    //        let currentItem = array()[index];
    //        let newItem = new HtmlTagItem(currentItem.value, currentItem.text, !currentItem.checked);
    //        array.replace(currentItem, newItem);
    //    }
    //}

    //public refresh() {
    //    this.getRule(0)
    //    //this.reccuringEvent('yes')
    //}

}

//export default RRuleViewModel 

