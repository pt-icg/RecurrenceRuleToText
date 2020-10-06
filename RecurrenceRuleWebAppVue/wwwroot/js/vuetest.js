//import { RRuleViewModel, HtmlTagItem } from "./RruleViewModel" // -> GET https://localhost:44389/js/RruleViewModel net::ERR_ABORTED 404
import { RRuleViewModel } from "./RruleViewModel.js"; //.js? https://github.com/microsoft/TypeScript/pull/35148 https://forum.freecodecamp.org/t/modules-pattern-es6-browser-cant-find-a-file-path/315998
import { RRuleWrapper } from "./RRuleTypes.js";
import { RRuleHttpClient } from "./rrule-http-client.js";
let rruleViewModel = new RRuleViewModel();
let v = new Vue({
    el: '#recurring-rule',
    data: rruleViewModel,
    mounted() {
        Vue.nextTick(function () {
            var btns = Array.from(document.getElementsByClassName('btn'));
            for (const x of btns) {
                const y = x;
                y.addEventListener("click", function () {
                    console.log("mounted click blur");
                    y.blur();
                });
                y.addEventListener("touchstart", function () {
                    console.log("mounted touchstart");
                    y.classList.remove("mobileHoverFix");
                });
                y.addEventListener("touchend", function () {
                    console.log("mounted touchend");
                    y.classList.add("mobileHoverFix");
                });
            }
        });
        //Vue.nextTick(function () {
        //    // I want to access props here but it return 0 length array 
        //    let vm = this;
        //    //console.log(vm.$parent.Subscriptions);
        //    $(".btn").on("click", function () {
        //        console.log("mounted click blur");
        //        $(this).blur(); // basically George's solution, but for all BS buttons
        //    });
        //    $(".btn").on("touchstart", function () {
        //        console.log("mounted touchstart");
        //        $(this).removeClass("mobileHoverFix");
        //    });
        //    $(".btn").on("touchend", function () {
        //        console.log("mounted touchend");
        //        $(this).addClass("mobileHoverFix");
        //    });
        //});
    },
    //beforeDestroy() {
    //    window.removeEventListener('resize', this.someMethod);
    //},
    methods: {
        ResetForm() {
            this.Starttime = new Date();
        },
        Toggle(htmlTagItem) {
            htmlTagItem.checked = !htmlTagItem.checked;
            this.SubmitForm();
        },
        SubmitForm() {
            let rRuleWrapper = getRRuleWrapper(rruleViewModel);
            let rRuleHttpClient = new RRuleHttpClient();
            rRuleHttpClient.createRRule(rRuleWrapper)
                .then((rRuleResult) => {
                console.log(rRuleResult.errorText);
                rruleViewModel.RRuleCode = rRuleResult.recurrencePatternString;
                rruleViewModel.RRuleText = rRuleResult.recurrencePatternText;
                rruleViewModel.RRuleOutput = getDateTimeArray(rRuleResult.recurrencePatternList);
                rruleViewModel.RRuleHint = rRuleResult.hintText;
                rruleViewModel.RRuleError = rRuleResult.errorText;
                //rruleViewModel.ShowReccuringEvent = true
            })
                .catch((error) => {
                //throw error;
                console.log(error.message);
                rruleViewModel.RRuleError = error.message;
                //rruleViewModel.ShowReccuringEvent = true
            });
            //getRRuleResult(rRuleWrapper)
            //    .then((response: AxiosResponse<RRuleResult>) => {
            //        const rRuleResult: RRuleResult = response.data;
            //        console.log(rRuleResult.errorText)
            //        rruleViewModel.RRuleCode = rRuleResult.recurrencePatternString
            //        rruleViewModel.RRuleText = rRuleResult.recurrencePatternText
            //        rruleViewModel.RRuleOutput = getDateTimeArray(rRuleResult.recurrencePatternList)
            //        rruleViewModel.RRuleHint = rRuleResult.hintText
            //        rruleViewModel.RRuleError = rRuleResult.errorText
            //    })
            //    .catch((error: AxiosError) => {
            //        //throw error;
            //        console.log(error.message)
            //        rruleViewModel.RRuleError = error.message
            //    });
        }
    }
});
v.SubmitForm();
function getRRuleWrapper(rRuleViewModel) {
    let rRuleWrapper = new RRuleWrapper();
    rRuleWrapper.StartDate = rRuleViewModel.Starttime;
    rRuleWrapper.Frequency = rRuleViewModel.SelectedFrequency;
    rRuleWrapper.Interval = rRuleViewModel.EveryRuleInterval;
    switch (rRuleWrapper.Frequency) {
        case "weekly":
            rRuleWrapper.ByDayValue = rRuleViewModel.WeekDays.filter(i => i.checked).map(i => i.value);
            break;
        case "monthly":
            switch (rRuleViewModel.MonthlyOptions) {
                case "monthly-days":
                    rRuleWrapper.ByMonthDay = rRuleViewModel.MonthDays.filter(i => i.checked).map(i => i.value);
                    break;
                case "monthly-precise":
                    rRuleWrapper.BySetPosition.push(rRuleViewModel.SelectedMonthByDayPos);
                    rRuleWrapper.ByDayValue.push(rRuleViewModel.SelectedMonthByDayPosName);
                    break;
            }
            break;
        case "yearly":
            switch (rRuleViewModel.YearlyOptions) {
                case "yearly-one-month":
                    rRuleWrapper.ByMonth.push(rRuleViewModel.SelectedYearlyByMonth);
                    rRuleWrapper.ByMonthDay.push(rRuleViewModel.SelectedYearlyByMonthDay);
                    break;
                case "yearly-multiple-months":
                    rRuleWrapper.ByMonth = rRuleViewModel.YearlyMultipleMonths.filter(i => i.checked).map(i => i.value);
                    break;
                case "yearly-precise":
                    rRuleWrapper.BySetPosition.push(rRuleViewModel.SelectedYearlyBySetPos);
                    rRuleWrapper.ByDayValue.push(rRuleViewModel.SelectedYearlyByDay);
                    rRuleWrapper.ByMonth.push(rRuleViewModel.SelectedYearlyByMonthWithBySetPosByDay);
                    break;
            }
            break;
    }
    switch (rRuleViewModel.SelectedEndRule) {
        case "never":
            break;
        case "occurrences":
            rRuleWrapper.Count = rRuleViewModel.EndRuleOccurrences;
            break;
        case "until":
            rRuleWrapper.Until = rRuleViewModel.EndRuleUntil; //erstmal nur für Format 2020-12-31
            break;
    }
    return rRuleWrapper;
}
//declare var axios: any;
//function getRRuleResult(rRuleWrapper: RRuleWrapper): AxiosPromise<RRuleResult> {
//    //let antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
//    const config = {
//        headers: {
//            'content-type': 'application/json;charset=UTF-8',
//            'Accept': 'application/json'
//            //"RequestVerificationToken": antiForgeryToken
//        }
//    }
//    return axios({
//        url: 'Home/CreateRRule',
//        method: 'post',
//        headers: config.headers,
//        data: rRuleWrapper
//    })
//}
function getDateTimeArray(list) {
    let result = new Array(list.length);
    const optionsWeekDay = { weekday: 'long' };
    const optionsDate = { year: 'numeric', month: '2-digit', day: '2-digit' };
    const optionsTime = { hour: '2-digit', minute: '2-digit' };
    for (let i = 0; i < list.length; i++) {
        let d = new Date(list[i]);
        result[i] = [d.toLocaleString(undefined, optionsWeekDay), d.toLocaleString(undefined, optionsDate), d.toLocaleString(undefined, optionsTime)];
    }
    return result;
}
//# sourceMappingURL=vuetest.js.map