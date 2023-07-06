const textElements = document.getElementsByClassName("text");
const btnChangeToBlue =
    document.getElementsByClassName("btn-change-to-blue")[0];
const btnChangeToGreen = document.getElementsByClassName(
    "btn-change-to-green"
)[0];
const btnChangeToNormal = document.getElementsByClassName(
    "btn-change-to-normal"
)[0];

btnChangeToBlue.addEventListener("click", () => {
    Array.from(textElements).forEach((elem) => {
        elem.classList.remove("text-success");
        elem.classList.remove("text-dark");
        elem.classList.add("text-primary");
    });
});

btnChangeToGreen.addEventListener("click", () => {
    Array.from(textElements).forEach((elem) => {
        elem.classList.remove("text-primary");
        elem.classList.remove("text-dark");
        elem.classList.add("text-success");
    });
});

btnChangeToNormal.addEventListener("click", () => {
    Array.from(textElements).forEach((elem) => {
        elem.classList.remove("text-success");
        elem.classList.remove("text-primary");
        elem.classList.add("text-dark");
    });
});
