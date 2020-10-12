//https://discuss.appstudio.dev/t/bootstrap-button-click-in-mobile-sticks/575/4
export function prepareBtn() {
    var btns = Array.from(document.getElementsByClassName('btn'));
    for (const btn of btns) {
        const hTMLElement = btn;
        hTMLElement.addEventListener("click", function () {
            console.log("mounted click blur");
            hTMLElement.blur();
        });
        hTMLElement.addEventListener("touchstart", function () {
            console.log("mounted touchstart");
            hTMLElement.classList.remove("mobileHoverFix");
        });
        hTMLElement.addEventListener("touchend", function () {
            console.log("mounted touchend");
            hTMLElement.classList.add("mobileHoverFix");
        });
    }
}
//# sourceMappingURL=ButtonGroupHelper.js.map