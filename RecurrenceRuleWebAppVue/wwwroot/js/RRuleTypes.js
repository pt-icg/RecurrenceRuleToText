export const IntMinValue = -2147483648; //Int32 MinValue = -2147483648 => default in RecurrencePattern.Count
export class RRuleWrapper {
    constructor() {
        this.Count = IntMinValue;
        this.Until = null;
        this.ByDayValue = [];
        this.ByMonth = [];
        this.ByMonthDay = [];
        this.BySetPosition = [];
    }
}
//# sourceMappingURL=RRuleTypes.js.map