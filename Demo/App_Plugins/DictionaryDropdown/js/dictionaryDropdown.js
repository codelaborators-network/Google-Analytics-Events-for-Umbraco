angular.module("umbraco").controller("Our.Umbraco.DictionaryValuesController",
    function ($scope) {

        //NOTE: We need to make each item an object, not just a string because you cannot 2-way bind to a primitive.

        $scope.newItem = {
            key: "",
            value: ""
        };
        $scope.hasError = false;

        if (!angular.isArray($scope.model.value)) {
            //make an array from the dictionary
            var items = [];
            for (var i in $scope.model.value) {
                items.push({
                    value: $scope.model.value[i],
                    key: i
                });
            }
            //now make the editor model the array
            $scope.model.value = items;
        }


        $scope.remove = function (item, evt) {

            evt.preventDefault();

            $scope.model.value = _.reject($scope.model.value, function (x) {
                return x.key === item.key && x.value === item.value;
            });

        };

        $scope.add = function (evt) {

            evt.preventDefault();

            if ($scope.newItem.key && $scope.newItem.value) {
                if (!_.contains($scope.model.value, function (x) {
                    return x.value === item.value;
                })) {
                    $scope.model.value.push({
                        key: $scope.newItem.key, 
                        value: $scope.newItem.value
                    });
                    $scope.newItem = {
                        key: "",
                        value: ""
                    };
                    $scope.hasError = false;
                    return;
                }
            }

            //there was an error, do the highlight (will be set back by the directive)
            $scope.hasError = true;
        };

    });

angular.module("umbraco").controller("Our.Umbraco.DropdownController",
    function ($scope) {

        //setup the default config
        var config = {
            items: [],
            multiple: false
        };

        //map the user config
        angular.extend(config, $scope.model.config);

        //map back to the model
        $scope.model.config = config;

        //now we need to check if the value is null/undefined, if it is we need to set it to "" so that any value that is set
        // to "" gets selected by default
        if ($scope.model.value === null || $scope.model.value === undefined) {
            if ($scope.model.config.multiple) {
                $scope.model.value = [];
            }
            else {
                $scope.model.value = "";
            }
        }

    });