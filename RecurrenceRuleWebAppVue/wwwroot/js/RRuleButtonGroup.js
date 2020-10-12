export const RRuleButtonGroup = Vue.component('rrule-button-group', {
    props: {
        items: {
            type: Array,
            required: true,
        },
        toggle: {
            type: Function,
            required: true,
        },
        disabled: false,
        buttonwidth: {
            type: Number,
            required: false,
        },
    },
    //template: `<div class="btn-group-toggle" data-toggle='buttons'>\
    //             <button class="btn btn-outline-secondary"\
    //                v-bind:id="item.id" v-bind:style="{ width: buttonwidth + 'px' }" v-for="item in items" v-bind:disabled="disabled" v-on:click="toggle(item)"\
    //                onclick="mobileHelperOnClick()" ontouchstart="mobileHelperOnTouchstart()" touchend="mobileHelperOnTouchend()">{{ item.text }}</button>\
    //          </div>`,
    template: `<div class="btn-group-toggle" data-toggle='buttons'>\
                 <button class="btn btn-outline-secondary" v-bind:id="item.id" v-bind:style="{ width: buttonwidth + 'px' }" v-for="item in items" v-bind:disabled="disabled" v-on:click="toggle(item)">{{ item.text }}</button>\
              </div>`,
});
//https://discuss.appstudio.dev/t/bootstrap-button-click-in-mobile-sticks/575/4
export function prepareBtnClass() {
    var btns = Array.from(document.getElementsByClassName('btn'));
    for (const btn of btns) {
        const hTMLElement = btn;
        hTMLElement.addEventListener("click", function () {
            hTMLElement.blur();
            let msg = `${hTMLElement.innerText}: mounted click blur`;
            console.log(msg);
        });
        hTMLElement.addEventListener("touchstart", function () {
            hTMLElement.classList.remove("mobileHoverFix");
            let msg = `${hTMLElement.innerText}: mounted touchstart`;
            console.log(msg);
        });
        hTMLElement.addEventListener("touchend", function () {
            hTMLElement.classList.add("mobileHoverFix");
            let msg = `${hTMLElement.innerText}: mounted touchend`;
            console.log(msg);
        });
    }
}
//# sourceMappingURL=RRuleButtonGroup.js.map