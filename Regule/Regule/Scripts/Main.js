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
    
    var x = document.getElementsByName("cpf");
    for (var i = 0; i < x.length; i++) {
        x[i].addEventListener("click", function () {
            var quadro = document.querySelector('input[name$=' + this.value.toUpperCase() + ']').parentElement.parentElement;
            quadro.querySelector('input[name$='+this.value.toUpperCase()+']').value = '';
            quadro.classList.remove("hidden");
            if (quadro.nextElementSibling.querySelector('[name$=CNPJ]')) {
                quadro.nextElementSibling.classList.add("hidden");
                quadro.nextElementSibling.querySelector('input[name$=CNPJ]').value = '00000000000000';
            } else {
                quadro.previousElementSibling.classList.add("hidden");
                quadro.previousElementSibling.querySelector('input[name$=CPF]').value = '00000000000';
            }
        })
    }
})();

function action(el, tipo) {
    if (tipo == 1) {
        indice--;
        tabela = el.parentElement.parentElement.parentElement;
        el.parentElement.parentElement.remove();
        for (var i = 1; i < tabela.children.length; i++) {
            var y = tabela.children[i];
            for (var j = 0; j < y.children.length; j++) {
                var x = y.children[j].querySelector('[name]');
                if (j == y.children.length - 1 && x) {
                    x.name = x.name.replace(/[0-9]+/, i - 1);
                    x.id = x.id.replace(/[0-9]+/, i - 1);
                    break;
                }
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