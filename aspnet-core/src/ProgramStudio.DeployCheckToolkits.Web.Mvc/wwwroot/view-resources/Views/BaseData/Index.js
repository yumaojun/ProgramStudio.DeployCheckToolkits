(function () {
    $(function () {
        //debugger
        var _$modal = $('#ProjectCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate();

        $('#RefreshButton').click(function () {
            refreshList();
        });

        $('#CutoverPlan').click(function () {
            window.location.href = abp.appPath + "CutoverPlan";
        });

        $('.delete-project').click(function () {
            var projectId = $(this).attr("data-project-id");
            var projectName = $(this).attr('data-project-name');

            deleteEntity(projectId, projectName);
        });

        $('.edit-project').click(function (e) {
            var projectId = $(this).attr("data-project-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'BaseData/EditModal?projectId=' + projectId,
                type: 'POST',
                dataType: 'html',
                contentType: 'application/html',
                success: function (content) {
                    $('#ProjectEditModal div.modal-content').html(content);
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
                url: abp.appPath + 'BaseData/Create',
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

        function deleteEntity(projectId, projectName) {
            let entity = { id: projectId };
            abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'DeployCheckToolkits'), projectName),
                function (isConfirmed) {
                    if (isConfirmed) {
                        abp.ajax({
                            url: abp.appPath + 'BaseData/Delete',
                            data: JSON.stringify(entity)
                        }).done(function () {
                            abp.notify.success('Delete with id = ' + entity.id);
                            refreshList();
                        });
                    }
                }
            );
        }
    });
})();
