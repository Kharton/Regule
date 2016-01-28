var indice = 0;
(function () {
    var botoes = document.getElementsByClassName('remover');
    for (var i = 0; i < botoes.length; i++) {
        botoes[i].addEventListener('click', function () { action(this, 1) });
    }
    botoes = document.getElementsByClassName('adicionar');
    for (var i = 0; i < botoes.length; i++) {
        botoes[i].addEventListener('click', function () { action(this, 2) });
    }
    indice = document.querySelectorAll('table:not(.hidden) .re-item').length;
})();

function action(el, tipo) {
    if (tipo == 1) {
        indice--;
        tabela = el.parentElement.parentElement.parentElement;
        el.parentElement.parentElement.remove();
        for (var i = 1; i < tabela.children.length; i++) {
            var y = tabela.children[i];
            for (var j = 0; j < y.children.length - 1; j++) {
                y.children[j].children[0].name = y.children[j].children[0].name.replace(/[0-9]+/, i - 1);
                y.children[j].children[0].id = y.children[j].children[0].id.replace(/[0-9]+/, i-1);
            }
        }

    } else if (tipo == 2) {
        y = document.querySelector('.sample tbody tr').cloneNode();
        y.innerHTML = document.querySelector('.sample tbody tr').innerHTML;
        for (var i = 0; i < y.children.length-1; i++) {
            y.children[i].children[0].name = y.children[i].children[0].name.replace(/[0-9]+/, indice);
            y.children[i].children[0].value = null;
            y.children[i].children[0].id = y.children[i].children[0].id.replace(/[0-9]+/, indice);
        }
        y.querySelector('.remover').addEventListener('click', function () {
            action(this, 1);
        });
        el.parentElement.parentElement.parentElement.appendChild(y);
        indice++;
    }
}