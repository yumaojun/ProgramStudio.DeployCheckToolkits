(function () {
    $(function () {
        //debugger
        var _$modal = $('#SettingCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate();

        $('#RefreshButton').click(function () {
            refreshList();
        });

        $('.delete-setting').click(function () {
            var settingId = $(this).attr("data-setting-id");
            var settingName = $(this).attr('data-setting-name');

            deleteEntity(settingId, settingName);
        });

        $('.edit-setting').click(function (e) {
            var settingId = $(this).attr("data-setting-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'Settings/EditSettingModal?settingId=' + settingId,
                type: 'POST',
                dataType: 'html',
                contentType: 'application/html',
                success: function (content) {
                    $('#SettingEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }
            //debugger
            var entity = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js

            abp.ui.setBusy(_$modal);
            abp.ajax({
                url: abp.appPath + 'Settings/Create',
                data: JSON.stringify(entity)
            }).done(function (data) {
                abp.notify.success('Created new with id = ' + data.id);
                _$modal.modal('hide');
                refreshList();
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });

        function refreshList() {
            location.reload(true); //reload page to see new!
        }

        function deleteEntity(settingId, settingName) {
            let entity = { id: settingId };
            abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'DeployCheckToolkits'), settingName),
                function (isConfirmed) {
                    if (isConfirmed) {
                        abp.ajax({
                            url: abp.appPath + 'Settings/Delete',
                            data: JSON.stringify(entity)
                        }).done(function (data) {
                            abp.notify.success('Delete with id = ' + entity.id);
                            _$modal.modal('hide');
                            refreshList();
                        });
                    }
                }
            );
        }
    });
})();
