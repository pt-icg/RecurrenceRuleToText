const intMinValue = -2147483648; //Int32 MinValue = -2147483648 => default in RecurrencePattern.Count

export class RRuleWrapper {
    StartDate: string
    Frequency: string
    Interval: number
    Count: number = intMinValue
    Until: string = null
    ByDayValue: string[] = []
    ByMonth: string[] = []
    ByMonthDay: string[] = []
    BySetPosition: string[] = []    
}

export interface RRuleWrapperOld {
    StartDate: string
    Frequency: string
    Interval: number
}

export interface RRuleResult {
    errorText: string
    recurrencePatternString: string
    recurrencePatternText: string
    recurrencePatternList: Array<string>
    hintText: string
}

