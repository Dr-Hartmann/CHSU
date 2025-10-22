package com.server.controller;

import jakarta.transaction.Transactional;
import jakarta.validation.Valid;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;
import java.util.function.BiConsumer;
import java.util.function.Function;

@RequestMapping(produces = MediaType.APPLICATION_JSON_VALUE)
public abstract class BaseController<E, R, C, U> {

    protected abstract JpaRepository<E, Long> repository();

    protected abstract Function<E, R> toRead();

    protected abstract Function<C, E> toCreate();

    protected abstract BiConsumer<U, E> update();

    @GetMapping
    public List<R> getAll() {
        return repository().findAll().stream()
                .map(toRead())
                .toList();
    }

    @GetMapping("/{id}")
    public ResponseEntity<R> getById(@PathVariable("id") Long id) {
        return repository().findById(id)
                .map(e -> ResponseEntity.ok(toRead().apply(e)))
                .orElse(ResponseEntity.notFound().build());
    }

    @PostMapping
    public R create(@RequestBody @Valid C dto) {
        return toRead().apply(repository().save(toCreate().apply(dto)));
    }

    @Transactional
    @PutMapping("/{id}")
    public ResponseEntity<R> update(@PathVariable("id") Long id, @RequestBody @Valid U dto) {
        return repository().findById(id)
                .map(e -> {
                    update().accept(dto, e);
                    repository().save(e);
                    return ResponseEntity.ok(toRead().apply(e));
                })
                .orElse(ResponseEntity.notFound().build());
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    public void delete(@PathVariable("id") Long id) {
        if (!repository().existsById(id)) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND);
        }
        repository().deleteById(id);
    }

}
