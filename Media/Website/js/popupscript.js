var popUpNumber = 0;

window.onload = (event) => {
    popUpNumber = localStorage.getItem("popUpNumber");
}

window.onunload = (event) => {
    localStorage.setItem("givenNumber", popUpNumber);
}

function FalseAlarm(){
    camId = localStorage.getItem('camId');
    updateData(camId);

    // TO MAKE: zorgen dat de camera via de map.removemarker weer van de map wordt gehaald
}