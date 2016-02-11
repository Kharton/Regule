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
                var x = y.children[j].querySelector('[name]');
                x.name = x.name.replace(/[0-9]+/, i - 1);
                x.id = x.id.replace(/[0-9]+/, i - 1);
            }
        }

    } else if (tipo == 2) {
        y = document.querySelector('.sample tbody tr').cloneNode();
        y.innerHTML = document.querySelector('.sample tbody tr').innerHTML;
        for (var i = 0; i < y.children.length - 1; i++) {
            var x = y.children[i].querySelector('[name]');
            x.name = x.name.replace(/[0-9]+/, indice);
            x.value = null;
            x.id = x.id.replace(/[0-9]+/, indice);
        }
        y.querySelector('.remover').addEventListener('click', function () {
            action(this, 1);
        });
        el.parentElement.parentElement.parentElement.appendChild(y);
        indice++;
    }
}