package com.server.repository;

import com.server.entity.Equipment;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface EquipmentRepository extends JpaRepository<Equipment, Long> {
    // @Override
    // @Transactional(timeout = 10)
    // public List<User> findAll();

    // @Lock(LockModeType.READ)
    // List<User> findAll();

    // Optional<Device> findBySerialNumber(String serialNumber);

    // @Query("select u from User u where u.emailAddress = ?1")
    // User findByEmailAddress(String emailAddress);

    // @Query("select u from User u where u.firstname like %?1")
    // List<User> findByFirstnameEndsWith(String firstname);

    // @Query(value = "SELECT * FROM USERS WHERE EMAIL_ADDRESS = ?1", nativeQuery =
    // true)
    // User findByEmailAddress(String emailAddress);

    // @Query("select u from User u where u.lastname like ?1%")
    // List<User> findByAndSort(String lastname, Sort sort);

    // @Query("select u.id, LENGTH(u.firstname) as fn_len from User u where
    // u.lastname like ?1%")
    // List<Object[]> findByAsArrayAndSort(String lastname, Sort sort);

    // @Query("select u from User u where u.firstname = :firstname or u.lastname =
    // :lastname")
    // User findByLastnameOrFirstname(@Param("lastname") String lastname,
    // @Param("firstname") String firstname);

    // @Query("select u from #{#entityName} u where u.lastname = ?1")
    // List<User> findByLastname(String lastname);

    // @Query("select u from User u where u.firstname = ?1 and u.firstname=?#{[0]}
    // // and u.emailAddress = ?#{principal.emailAddress}")
    // List<User> findByFirstnameAndCurrentUserWithCustomQuery(String firstname);

    // @Query("select u from User u where u.lastname like %:#{[0]}% and u.lastname
    // like %:lastname%")
    // List<User> findByLastnameWithSpelExpression(@Param("lastname") String
    // lastname);

    // @Query("select u from User u where u.firstname like %?#{escape([0])}% escape
    // ?#{escapeCharacter()}")
    // List<User> findContainingEscaped(String namePart);

    // @Modifying
    // @Query("update User u set u.firstname = ?1 where u.lastname = ?2")
    // int setFixedFirstnameFor(String firstname, String lastname);

    // @Modifying
    // @Query("delete from User u where u.role.id = ?1")
    // void deleteInBulkByRoleId(long roleId);
}
