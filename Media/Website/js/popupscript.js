var popUpNumber = 0;

window.onload = (event) => {
    popUpNumber = localStorage.getItem("popUpNumber");
    console.log(localStorage.getItem("popUpNumber"));
    console.log(localStorage.getItem([popUpNumber]));
}

window.onunload = (event) => {
    localStorage.setItem("givenNumber", popUpNumber);
}

function FalseAlarm(){
    
}