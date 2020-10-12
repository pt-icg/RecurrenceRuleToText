import { IntMinValue, HtmlTagItem, HtmlTagItemActiveClass } from "./RRuleTypes.js";
//export class HtmlTagItem {
//    public value: string
//    public text: string
//    public checked: boolean
//    constructor(value: string, text: string, checked: boolean = false) {
//        this.value = value
//        this.text = text
//        this.checked = checked
//    }
//}
const days = 31;
const frequencyEnumBase = 4;
const weekDayArray = ['MO', 'TU', 'WE', 'TH', 'FR', 'SA', 'SU'];
const minDateTime = '0001-01-01T00:00:00';
export class RRuleViewModel {
    constructor() {
        this.ShowAfterBoot = false;
        this.ShowReccuringEvent = false;
        this.ShowReccuringEvent = true;
        const today = new Date();
        var dateTimeString = today.getFullYear() + '-' + ('0' + (today.getMonth() + 1)).slice(-2) + '-' + ('0' + today.getDate()).slice(-2) + 'T' + ('0' + today.getHours()).slice(-2) + ':' + ('0' + today.getMinutes()).slice(-2) + ':00';
        this.Starttime = dateTimeString;
        this.Frequencyies = [new HtmlTagItem('daily', 'Daily'), new HtmlTagItem('weekly', 'Weekly'), new HtmlTagItem('monthly', 'Monthly'), new HtmlTagItem('yearly', 'Yearly')];
        this.SelectedFrequency = this.Frequencyies[0].value;
        //WEEKLY
        this.WeekDays = [
            //new HtmlTagItem('MO', 'Mon', false),
            //new HtmlTagItem('TU', 'Tue', false),
            //new HtmlTagItem('WE', 'Wed', false),
            //new HtmlTagItem('TH', 'Thu', false),
            //new HtmlTagItem('FR', 'Fri', false),
            //new HtmlTagItem('SA', 'Sat', false),
            //new HtmlTagItem('SU', 'Sun', false),
            new HtmlTagItemActiveClass(1, 'WeekDay', 'MO', 'Mon', false),
            new HtmlTagItemActiveClass(2, 'WeekDay', 'TU', 'Tue', false),
            new HtmlTagItemActiveClass(3, 'WeekDay', 'WE', 'Wed', false),
            new HtmlTagItemActiveClass(4, 'WeekDay', 'TH', 'Thu', false),
            new HtmlTagItemActiveClass(5, 'WeekDay', 'FR', 'Fri', false),
            new HtmlTagItemActiveClass(6, 'WeekDay', 'SA', 'Sat', false),
            new HtmlTagItemActiveClass(7, 'WeekDay', 'SU', 'Sun', false),
        ];
        ////MONTHLY
        this.MonthlyOptions = "monthly-days";
        ////MONTHLY on
        //this.MonthDays = new Array<HtmlTagItem>(days);
        //for (let i = days; i--;) this.MonthDays[i] = new HtmlTagItem((i + 1).toString(), (i + 1).toString(), false);
        this.MonthDays = new Array(days);
        for (let i = days; i--;)
            this.MonthDays[i] = new HtmlTagItemActiveClass(i + 1, 'MonthDays', (i + 1).toString(), (i + 1).toString(), false);
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
            //new HtmlTagItem((1).toString(), 'Jan', false),
            //new HtmlTagItem((2).toString(), 'Feb', false),
            //new HtmlTagItem((3).toString(), 'Mar', false),
            //new HtmlTagItem((4).toString(), 'Apr', false),
            //new HtmlTagItem((5).toString(), 'May', false),
            //new HtmlTagItem((6).toString(), 'Jun', false),
            //new HtmlTagItem((7).toString(), 'Jul', false),
            //new HtmlTagItem((8).toString(), 'Aug', false),
            //new HtmlTagItem((9).toString(), 'Sep', false),
            //new HtmlTagItem((10).toString(), 'Oct', false),
            //new HtmlTagItem((11).toString(), 'Nov', false),
            //new HtmlTagItem((12).toString(), 'Dec', false)
            new HtmlTagItemActiveClass(1, 'YearlyMultipleMonths', (1).toString(), 'Jan', false),
            new HtmlTagItemActiveClass(2, 'YearlyMultipleMonths', (2).toString(), 'Feb', false),
            new HtmlTagItemActiveClass(3, 'YearlyMultipleMonths', (3).toString(), 'Mar', false),
            new HtmlTagItemActiveClass(4, 'YearlyMultipleMonths', (4).toString(), 'Apr', false),
            new HtmlTagItemActiveClass(5, 'YearlyMultipleMonths', (5).toString(), 'May', false),
            new HtmlTagItemActiveClass(6, 'YearlyMultipleMonths', (6).toString(), 'Jun', false),
            new HtmlTagItemActiveClass(7, 'YearlyMultipleMonths', (7).toString(), 'Jul', false),
            new HtmlTagItemActiveClass(8, 'YearlyMultipleMonths', (8).toString(), 'Aug', false),
            new HtmlTagItemActiveClass(9, 'YearlyMultipleMonths', (9).toString(), 'Sep', false),
            new HtmlTagItemActiveClass(10, 'YearlyMultipleMonths', (10).toString(), 'Oct', false),
            new HtmlTagItemActiveClass(11, 'YearlyMultipleMonths', (11).toString(), 'Nov', false),
            new HtmlTagItemActiveClass(12, 'YearlyMultipleMonths', (12).toString(), 'Dec', false)
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
        this.NewRRuleCode = "";
        this.BtnProblemInfo = "";
    }
    Initialize() {
        /*
        this.Frequencyies = [new HtmlTagItem('daily', 'Daily'), new HtmlTagItem('weekly', 'Weekly'), new HtmlTagItem('monthly', 'Monthly'), new HtmlTagItem('yearly', 'Yearly')]
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

        this.MonthByDayPosName = [
            new HtmlTagItem('MO', 'Monday'),
            new HtmlTagItem('TU', 'Tuesday'),
            new HtmlTagItem('WE', 'Wednesday'),
            new HtmlTagItem('TH', 'Thursday'),
            new HtmlTagItem('FR', 'Friday'),
            new HtmlTagItem('SA', 'Saturday'),
            new HtmlTagItem('SU', 'Sunday')
        ];

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

        this.YearlyByMonthDay = new Array<HtmlTagItem>(days)
        for (let i = days; i--;) this.YearlyByMonthDay[i] = new HtmlTagItem((i + 1).toString(), (i + 1).toString(), false);

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

        this.YearlyByDay = [
            new HtmlTagItem('MO', 'Monday'),
            new HtmlTagItem('TU', 'Tuesday'),
            new HtmlTagItem('WE', 'Wednesday'),
            new HtmlTagItem('TH', 'Thursday'),
            new HtmlTagItem('FR', 'Friday'),
            new HtmlTagItem('SA', 'Saturday'),
            new HtmlTagItem('SU', 'Sunday')
        ]

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

        //end rules
        this.EndRules = [
            new HtmlTagItem('never', 'Never'),
            new HtmlTagItem('occurrences', 'Occurrences'),
            new HtmlTagItem('until', 'Until')
        ];
        */
    }
    convertDayOfWeekEnum(value) {
        let index = 6; //'SU'
        if (value > 0) //'MO'...'SA'
            index = value - 1;
        return weekDayArray[index];
    }
    assignRuleValue(recurrencePattern) {
        this.ShowReccuringEvent = true;
        this.SelectedFrequency = this.Frequencyies[recurrencePattern.Frequency - frequencyEnumBase].value;
        this.EveryRuleInterval = recurrencePattern.Interval;
        let byday = recurrencePattern.ByDay.map(i => this.convertDayOfWeekEnum(i.DayOfWeek));
        ////let byday = recurrencePattern.ByDay.map(i => {
        ////    let dayOfWeek = i.dayOfWeek
        ////    this.convertDayOfWeekEnum(dayOfWeek)
        ////});
        //let frequency = this.selectedFrequency().value;
        switch (this.SelectedFrequency) {
            case "weekly":
                this.WeekDays.forEach((item) => {
                    //item.checked = byday.indexOf(item.value) > -1
                    item.Initialize(byday.indexOf(item.value) > -1);
                });
                break;
            case "monthly":
                this.MonthDays.forEach((item) => {
                    //item.checked = false
                    item.Initialize(false);
                });
                if (recurrencePattern.ByMonthDay.length > 0) {
                    this.MonthlyOptions = "monthly-days";
                    recurrencePattern.ByMonthDay.forEach((item) => {
                        //this.MonthDays[item - 1].checked = true
                        this.MonthDays[item - 1].Initialize(true);
                        //item.checked = this.MonthDays[item - 1]) > -1
                    });
                }
                else {
                    this.MonthlyOptions = "monthly-precise";
                    //this.selectedMonthByDayPos(this.monthByDayPos().filter(i => recurrencePattern.BySetPosition.includes(i.value))[0]);
                    //this.selectedMonthByDayPosName(this.monthByDayPosName().filter(i => byday.includes(i.value))[0]);
                    if (recurrencePattern.BySetPosition.length > 0)
                        this.SelectedMonthByDayPos = this.MonthByDayPos.filter(i => Number(i.value) === recurrencePattern.BySetPosition[0])[0].value;
                    if (byday.length > 0)
                        this.SelectedMonthByDayPosName = this.MonthByDayPosName.filter(i => i.value === byday[0])[0].value;
                }
                break;
            case "yearly":
                this.YearlyMultipleMonths.forEach((item) => {
                    item.Initialize(false);
                });
                if (recurrencePattern.ByMonth.length > 0 && recurrencePattern.ByMonthDay.length > 0) {
                    this.YearlyOptions = "yearly-one-month";
                    //this.selectedYearlyByMonth(this.yearlyByMonth().filter(i => recurrencePattern.ByMonth.includes(i.value))[0]);
                    //this.selectedYearlyByMonthDay(this.yearlyByMonthDay().filter(i => recurrencePattern.ByMonthDay.includes(i.value))[0]);
                    this.SelectedYearlyByMonth = this.YearlyByMonth.filter(i => recurrencePattern.ByMonth.indexOf(Number(i.value)) > -1)[0].value;
                    this.SelectedYearlyByMonthDay = this.YearlyByMonthDay.filter(i => recurrencePattern.ByMonthDay.indexOf(Number(i.value)) > -1)[0].value;
                }
                else if (recurrencePattern.BySetPosition.length > 0 && byday.length > 0 && recurrencePattern.ByMonth.length > 0) {
                    this.YearlyOptions = "yearly-precise";
                    //this.selectedYearlyBySetPos(this.yearlyBySetPos().filter(i => recurrencePattern.BySetPosition.includes(i.value))[0]);
                    //this.selectedYearlyByDay(this.yearlyByDay().filter(i => byday.includes(i.value))[0]);
                    //this.selectedYearlyByMonthWithBySetPosByDay(this.yearlyByMonthWithBySetPosByDay().filter(i => recurrencePattern.ByMonth.includes(i.value))[0]);
                    this.SelectedYearlyBySetPos = this.YearlyBySetPos.filter(i => recurrencePattern.BySetPosition.indexOf(Number(i.value)) > -1)[0].value;
                    this.SelectedYearlyByDay = this.YearlyByDay.filter(i => i.value === byday[0])[0].value;
                    this.SelectedYearlyByMonthWithBySetPosByDay = this.YearlyByMonthWithBySetPosByDay.filter(i => recurrencePattern.ByMonth.indexOf(Number(i.value)) > -1)[0].value;
                }
                else if (recurrencePattern.ByMonth.length > 0) {
                    this.YearlyOptions = "yearly-multiple-months";
                    //this.yearlyMultipleMonths().filter(i => recurrencePattern.ByMonth.includes(i.value)).forEach((item, index) => {
                    //    this.checkButtonItem(item, this.yearlyMultipleMonths);
                    //});
                    //this.YearlyMultipleMonths.forEach((item) => {
                    //    item.checked = false
                    //})
                    if (recurrencePattern.ByMonth.length > 0) {
                        recurrencePattern.ByMonth.forEach((item) => {
                            //this.YearlyMultipleMonths[item - 1].checked = true
                            this.YearlyMultipleMonths[item - 1].Initialize(true);
                        });
                    }
                }
                break;
        }
        if (recurrencePattern.Count > IntMinValue) {
            this.SelectedEndRule = this.EndRules[1].value;
            this.EndRuleOccurrences = recurrencePattern.Count;
        }
        else if ((recurrencePattern.Until.length > 0) && (recurrencePattern.Until !== minDateTime)) {
            this.SelectedEndRule = this.EndRules[2].value;
            this.EndRuleUntil = recurrencePattern.Until.substring(0, 10);
            //2020-09-30T00:00:00+02:00 -> 2020-09-30
        }
    }
}
//# sourceMappingURL=RRuleViewModel.js.map