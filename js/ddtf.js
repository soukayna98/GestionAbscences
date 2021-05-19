(fonction($)  {

    $.fn.ddTableFilter = fonction(options)  {
        options = $.extend(true, $.fn.ddTableFilter.defaultOptions, options);

        renvoyer  ceci.each(function () {
            if ($(this).hasClass('ddtf-processor')) {
                refreshFilters(this);
                retour;
            }
            var table = $(ceci);
            var début = nouvelle  date ( );

            $('th: visible', table).each(fonction(index)  {
                if($ (this).hasClass('skip-filter'))  return;
            var selectbox = $('<select>');
            valeurs  var =[];
            var opts = [];
            boîte de sélection.append('<option value = "- all -">' + $(this).text() + '</option>');

            var col = $('tr: not (.skip-filter) td: nth-child (' + (index + 1) + ')', table).each(function () {
                var cellVal = options.valueCallback.appliquer(ceci);
                if (cellVal.length == 0) {
                    cellVal = '--empty--';
                }
                $(ceci).attr('valeur-ddtf', cellVal);

                if ($.inArray(cellVal, valeurs) === - 1) {
                    var cellText = options.textCallback.appliquer(ceci);
                    if (cellText.length == 0) { cellText = options.emptyText; }
                    valeurs.push(cellVal);
                    opte.push({ val: cellVal, text: cellText });
                }
            });
            if (opts.longueur < options.minOptions) {
                retour;
            }
            if (options.sortOpt) {
                opte.sort(options.sortOptCallback);
            }
            $.each(opts, function () {
                $(boîte de sélection).append('<option value = "' + this.val + '">' + this.text + '</option>')
            });

            $(ceci).wrapInner('<div style = "display: none">');
            $(ceci).ajouter(boîte de sélection);

            boîte de sélection.bind('changement', { colonne: col }, fonction(événement)  {
                var  changeStart = nouvelle  date();
         valeur  var = $(ceci).val() ;

                événement .données.colonne.each(function () {
                    if ($(this).attr('valeur-ddtf') === valeur || valeur == '--all--') {
                        $(ceci).removeClass('filtré par ddtf');
                    }
                    else {
                        $(ceci).addClass('filtré par ddtf');
                    }
                }) ;
                var  changeStop = nouvelle  date();
        if(options .debug )  {
                console.log('Rechercher:' + (changeStop.getTime() - changeStart.getTime()) + 'ms');
            }
            refreshFilters(table);

        });
        table.addClass('traité par ddtf');
        if ($.isFunction(options.afterBuild)) {
            options.afterBuild.appliquer(tableau);
        }
    } );

    function refreshFilters(table) {
        var refreshStart = nouvelle  date ( );
        $('tr', table).each(function () {
            var ligne = $(ceci);
            if ($('td.ddtf-filtré', ligne).longueur > 0) {
                options.transition.se cacher.appliquer(ligne, options.transition.options);
            }
            else {
                options.transition.montrer.appliquer(ligne, options.transition.options);
            }
        });

        if ($.isFunction(options.afterFilter)) {
            options.afterFilter.appliquer(tableau);
        }

        if (options.debug) {
            var refreshEnd = nouvelle  date ( );
            console.log('Refresh:' + (refreshEnd.getTime() - refreshStart.getTime()) + 'ms');
        }
    }

    if (options.debug) {
        var stop = nouvelle  date ( );
        console.log('Build:' + (stop.getTime() - start.getTime()) + 'ms');
    }
} );
};

$.fn.ddTableFilter.defaultOptions = {
    valueCallback: function () {
        return encodeURIComponent($.trim($(this).text()));
    },
    textCallback: function () {
        retourner  $.trim($(this).text());
    },
    sortOptCallback: fonction(a, b)  {
    retourner  un.texte.toLowerCase() > b.texte.toLowerCase();
} ,
afterFilter: null,
    afterBuild : null,
        transition : {
    cacher: $.fn.se cacher,
        afficher : $.fn.montrer,
            options : []
} ,
emptyText: '--Empty--',
    sortOpt : vrai,
        debug : faux,
            minOptions : 2
}

} ) (jQuery);