-module(cone_combination_builder).
-export([cone_combinations/2, cone_combinations/1, most_popular_combinations/2]).

%% Part 1: One from List1 and one from List2
cone_combinations(List1, List2) ->
    [ {Flavor1, Flavor2} || Flavor1 <- List1, Flavor2 <- List2 ].

%% Part 2: Any combination from one list (including duplicates)
cone_combinations(FlavorList) ->
    [ {Top, Bottom} || Top <- FlavorList, Bottom <- FlavorList ].

%% Part 3: Return formatted strings for top N combinations
most_popular_combinations(Count, List) ->
    {Keepers, _Rest} = lists:split(Count, List),
    [ lists:flatten(io_lib:format("The ~s-~s cone was ordered ~p times!", [Top, Bottom, Purchases]))
      || {Purchases, {Top, Bottom}} <- Keepers ].

%% ===========================
%% ========== TESTS =========
%% ===========================
-ifdef(EUNIT).
-include_lib("eunit/include/eunit.hrl").

%% Tests for cone_combinations/2
cone_combinations_2_test() ->
    List1 = ["Vanilla", "Chocolate", "Cherry Ripple"],
    List2 = ["Lemon", "Butterscotch", "Licorice Ripple"],
    Result = cone_combinations(List1, List2),
    ExpectedLength = length(List1) * length(List2),
    ?assertEqual(ExpectedLength, length(Result)),
    ?assert(lists:member({"Chocolate", "Lemon"}, Result)),
    ?assert(lists:member({"Cherry Ripple", "Licorice Ripple"}, Result)).

%% Tests for cone_combinations/1
cone_combinations_1_test() ->
    Flavors = ["Vanilla", "Chocolate"],
    Result = cone_combinations(Flavors),
    Expected = [
        {"Vanilla", "Vanilla"},
        {"Vanilla", "Chocolate"},
        {"Chocolate", "Vanilla"},
        {"Chocolate", "Chocolate"}
    ],
    ?assertEqual(lists:sort(Expected), lists:sort(Result)).

%% Tests for most_popular_combinations/2
most_popular_combinations_test() ->
    Data = [
        {10, {"Chocolate", "Vanilla"}},
        {8, {"Vanilla", "Chocolate"}},
        {6, {"Lemon", "Butterscotch"}}
    ],
    Result = most_popular_combinations(2, Data),
    ?assertEqual("The Chocolate-Vanilla cone was ordered 10 times!", lists:nth(1, Result)),
    ?assertEqual("The Vanilla-Chocolate cone was ordered 8 times!", lists:nth(2, Result)).
-endif.
