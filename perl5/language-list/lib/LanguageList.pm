package LanguageList;
use strict;
use warnings;
use v5.38;

our @Languages;

sub add_language ($language) {
    return push(@Languages, $language);
}

sub remove_language () {
    return pop(@Languages);
}

sub first_language () {
    return @Languages[0];
}

sub last_language () {
    return @Languages[-1];
}

sub get_languages (@elements) {
    return map { $Languages[$_-1] } @elements;
}

sub has_language ($language) {
    return grep { $_ eq $language} @Languages;
}

1;
