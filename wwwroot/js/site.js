const uri = 'api/advices/random';
let todos = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function _displayItems(data) {
    const tBody = document.getElementById('advice');
    tBody.innerHTML = '';

    let tr = tBody.insertRow();

    let td2 = tr.insertCell(0);
    let textNode = document.createTextNode(data.text);
    td2.appendChild(textNode);

    // data.forEach(item => {
    //     let tr = tBody.insertRow();

    //     let td2 = tr.insertCell(0);
    //     let textNode = document.createTextNode(item.text);
    //     td2.appendChild(textNode);
    // });

    todos = data;
}