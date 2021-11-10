var popUpNumber = 0;
var falseAlert = document.getElementById('FalseAlarm');

window.onload = (event) => {
    popUpNumber = localStorage.getItem("popUpNumber");
}

window.onunload = (event) => {
    localStorage.setItem("givenNumber", popUpNumber);
}

falseAlert.on('click', function(){
    console.log('test');
});