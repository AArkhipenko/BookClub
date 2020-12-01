--
-- PostgreSQL database dump
--

-- Dumped from database version 12.4
-- Dumped by pg_dump version 12.4

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE IF EXISTS bookclub;
--
-- Name: bookclub; Type: DATABASE; Schema: -; Owner: -
--

CREATE DATABASE bookclub WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Russian_Russia.1251' LC_CTYPE = 'Russian_Russia.1251';


\connect bookclub

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: public; Type: SCHEMA; Schema: -; Owner: -
--

CREATE SCHEMA public;


--
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


--
-- Name: book; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.book (
    id integer NOT NULL,
    name text NOT NULL
);


--
-- Name: book_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.book_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- Name: book_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.book_id_seq OWNED BY public.book.id;


--
-- Name: lnk_user_book; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.lnk_user_book (
    id integer NOT NULL,
    user_id integer NOT NULL,
    book_id integer NOT NULL
);


--
-- Name: lnk_user_book_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.lnk_user_book_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- Name: lnk_user_book_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.lnk_user_book_id_seq OWNED BY public.lnk_user_book.id;


--
-- Name: user; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."user" (
    id integer NOT NULL,
    login text NOT NULL,
    password text NOT NULL
);


--
-- Name: user_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- Name: user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.user_id_seq OWNED BY public."user".id;


--
-- Name: book id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.book ALTER COLUMN id SET DEFAULT nextval('public.book_id_seq'::regclass);


--
-- Name: lnk_user_book id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.lnk_user_book ALTER COLUMN id SET DEFAULT nextval('public.lnk_user_book_id_seq'::regclass);


--
-- Name: user id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."user" ALTER COLUMN id SET DEFAULT nextval('public.user_id_seq'::regclass);


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: -
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20201112182308_Initial	3.1.4
\.


--
-- Data for Name: book; Type: TABLE DATA; Schema: public; Owner: -
--

COPY public.book (id, name) FROM stdin;
1	Цветы для Элджернона
2	Ася
3	Повелитель мух
4	Два капитана
\.


--
-- Data for Name: lnk_user_book; Type: TABLE DATA; Schema: public; Owner: -
--

COPY public.lnk_user_book (id, user_id, book_id) FROM stdin;
5	3	2
7	1	2
8	1	3
\.


--
-- Data for Name: user; Type: TABLE DATA; Schema: public; Owner: -
--

COPY public."user" (id, login, password) FROM stdin;
1	admin	admin
2	user	user
3	alex	alex
\.


--
-- Name: book_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.book_id_seq', 4, true);


--
-- Name: lnk_user_book_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.lnk_user_book_id_seq', 8, true);


--
-- Name: user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.user_id_seq', 3, true);


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: lnk_user_book UQ_user_book; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.lnk_user_book
    ADD CONSTRAINT "UQ_user_book" UNIQUE (user_id, book_id);


--
-- Name: book book_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.book
    ADD CONSTRAINT book_pkey PRIMARY KEY (id);


--
-- Name: lnk_user_book lnk_user_book_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.lnk_user_book
    ADD CONSTRAINT lnk_user_book_pkey PRIMARY KEY (id);


--
-- Name: user user_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pkey PRIMARY KEY (id);


--
-- Name: fki_FK_lnk_user_book_book; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "fki_FK_lnk_user_book_book" ON public.lnk_user_book USING btree (book_id);


--
-- Name: fki_FK_lnk_user_book_user; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "fki_FK_lnk_user_book_user" ON public.lnk_user_book USING btree (user_id);


--
-- Name: lnk_user_book FK_lnk_user_book_book; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.lnk_user_book
    ADD CONSTRAINT "FK_lnk_user_book_book" FOREIGN KEY (book_id) REFERENCES public.book(id);


--
-- Name: lnk_user_book FK_lnk_user_book_user; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.lnk_user_book
    ADD CONSTRAINT "FK_lnk_user_book_user" FOREIGN KEY (user_id) REFERENCES public."user"(id);


--
-- PostgreSQL database dump complete
--

