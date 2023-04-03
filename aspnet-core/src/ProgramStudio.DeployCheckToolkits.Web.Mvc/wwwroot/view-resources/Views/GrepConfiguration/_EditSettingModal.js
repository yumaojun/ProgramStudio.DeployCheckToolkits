(function ($) {
    //debugger
    var _$modal = $('#SettingEditModal');
    var _$form = $('form[name=SettingEditForm]');

    function save() {
        
        if (!_$form.valid()) {
            return;
        }

        var entity = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js

        abp.ui.setBusy(_$form);
        abp.ajax({
            url: abp.appPath + 'Settings/Update',
            data: JSON.stringify(entity)
        }).done(function (data) {
            abp.notify.success('Update changes with id = ' + data.id);
            _$modal.modal('hide');
            location.reload(true);
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    }

    //Handle save button click
    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    //Handle enter key
    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    $.AdminBSB.input.activate(_$form);

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);