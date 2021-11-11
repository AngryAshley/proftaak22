var popUpNumber = 0;

window.onload = (event) => {
    popUpNumber = localStorage.getItem("popUpNumber");
}

window.onunload = (event) => {
    localStorage.setItem("givenNumber", popUpNumber);
}

function FalseAlarm(){
    camId = localStorage.getItem('camId');
    //cam = localStorage.getItem('cam');

    //map.removeLayer(cam);
    
    updateData(camId);
}