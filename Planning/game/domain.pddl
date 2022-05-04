(define (domain game)
    (:requirements :strips :equality :typing)
    (:types location item character)
    (:predicates
        (at ?a ?b)
        (has ?a ?b)
        (path ?a ?b)
        (is-weapon ?i)
        (dead ?c)
        (alive ?c))

    (:action go
        :parameters (?c - character ?a ?b - location)
        :precondition (and
            (at ?c ?a)
            (path ?a ?b)
            (alive ?c))
        :effect (and
            (not (at ?c ?a))
            (at ?c ?b)))

    (:action get
        :parameters (?c - character ?i - item ?l - location)
        :precondition (and
            (at ?c ?l)
            (at ?i ?l)
            (alive ?c))
        :effect (and
            (has ?c ?i)
            (not (at ?i ?l))))

    (:action kill
        :parameters (?c ?t - character ?i - item ?l - location)
        :precondition (and
            (at ?c ?l)
            (at ?t ?l)
            (has ?c ?i)
            (alive ?t)
            (alive ?c)
            (is-weapon ?i))
        :effect (and
            (dead ?t)
            (not (alive ?t))))
)