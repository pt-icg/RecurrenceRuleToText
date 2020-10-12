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
//export class HtmlTagItem {
//    public value: string
//    public text: string
//    protected _checked: boolean
//    constructor(value: string, text: string, checked: boolean = false) {
//        this.value = value
//        this.text = text
//        this._checked = checked
//    }
//    public get checked() {
//        return this._checked;
//    }
//    public set checked(value: boolean) {
//        this._checked = value;
//    }
//}
//const activeClass = 'active'
////Problems when binding v-bind:class="{active: item.checked}" -> read/set active 
//export class HtmlTagItemActiveClass extends HtmlTagItem {
//    public readonly id: string
//    private hTMLElement: HTMLElement = null
//    constructor(index: number, groupname: string, value: string, text: string, checked: boolean = false) {
//        super(value, text, checked);
//        this.id = `${groupname}${index}`
//    }
//    private readHTMLElement() {
//        if (this.hTMLElement === null) {
//            this.hTMLElement = document.getElementById(this.id)
//        }
//    }
//    public get checked() {
//        //read from class 'active'
//        this.readHTMLElement()
//        if (this.hTMLElement === null) //nur vor vollständiger initialisierung und bei Nutzung von checked zur Anzeige
//            return this._checked;
//        else {
//            var classes = this.hTMLElement.className.split(" ");
//            this._checked = classes.indexOf(activeClass) > -1
//            //alert(`${this.id}: ${activeClass}? ${this.checked}`)
//            return this._checked;
//        }
//    }
//    public set checked(value: boolean) {
//        this.readHTMLElement()
//        this._checked = value
//        if (this._checked)
//            this.hTMLElement.classList.add(activeClass)
//        else
//            this.hTMLElement.classList.remove(activeClass)
//    }
//    //public SetActive(active: boolean) {
//    //    this.readHTMLElement()
//    //    this.checked = active
//    //    if (this.checked)
//    //        this.hTMLElement.classList.add(activeClass)
//    //    else
//    //        this.hTMLElement.classList.remove(activeClass)
//    //}
//    //public ReadActive() {
//    //    this.readHTMLElement()
//    //    var classes = this.hTMLElement.className.split(" ");
//    //    console.log(classes)
//    //    //alert(`${this.id}: ${this.hTMLElement.className}`)
//    //    //alert(`${this.id}: ${splitted}`)
//    //    this.checked = classes.indexOf(activeClass) > -1
//    //    console.log(classes.indexOf(activeClass) > -1);
//    //    alert(`${this.id}: ${activeClass}? ${this.checked}`)
//    //    //this.checked = true
//    //}
//}
export class HtmlTagItem {
    constructor(value, text, checked = false) {
        this.value = value;
        this.text = text;
        this.checked = checked;
    }
}
export class HtmlTagItemActiveClass extends HtmlTagItem {
    constructor(index, groupname, value, text, checked = false) {
        super(value, text, checked);
        this.hTMLElement = null;
        this.activeClass = 'active';
        this.id = `${groupname}${index}`;
    }
    readHTMLElement() {
        if (this.hTMLElement === null) {
            this.hTMLElement = document.getElementById(this.id);
        }
    }
    Initialize(checked) {
        this.readHTMLElement();
        this.checked = checked;
        if (this.checked)
            this.hTMLElement.classList.add(this.activeClass);
        else
            this.hTMLElement.classList.remove(this.activeClass);
    }
}
//# sourceMappingURL=RRuleTypes.js.map