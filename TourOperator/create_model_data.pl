#!/usr/bin/perl

use warnings;
use strict;

my $out_file_name = "./Contexts/DbModelCreator.cs";

my $template = <<END;
using Microsoft.EntityFrameworkCore;

using TourOperator.Models.Entities;

namespace TourOperator.Contexts;

public static class DbModelCreator
{
    public static void CreateModels(ModelBuilder modelBuilder) {
%s
    }
}
END

my $room_spaces = 20;

my %tours = (
    'Real Britain' => ['a tour', 6, 1200, 30],
    'Britain and Ireland Explorer' => ['a tour', 16, 2000, 40],
    'Best of Britain' => ['a tour', 12, 2900, 30]
);

my %hotels = (
    'Hilton London Hotel' => ['Hilton', 'a hotel', 375, 775, 950],
    'London Marriott Hotel' => ['Marriott', 'a hotel', 300, 500, 900],
    'Travelodge Brighton Seafront' => ['Travelodge', 'a hotel', 80, 120, 150],
    'Kings Hotel Brighton' => ['Kings', 'a hotel', 180, 400, 520],
    'Leonardo Hotel Brighton' => ['Leonardo', 'a hotel', 180, 400, 520],
    'Nevis Bank Inn, Fort William' => ['Independent', 'a hotel', 90, 100, 155]
);

my $content = "\t\t";

$content .= <<END;
modelBuilder.Entity<Customer>().HasData(
    new Customer{Id = 1, Username = "rhys", FullName="Rhys Adams", Password = "d74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1"}
);
END

my $id=1;
$content .= "\nmodelBuilder.Entity<Tour>().HasData(";
while(my ($name, $info) = each(%tours)) {
    my ($description, $length, $cost, $spaces) = @$info;
    $content .= "\n\tnew Tour{Id = $id, Name = \"$name\", Description = \"$description\", Length = $length, Price = @{[$cost * 100]}, Spaces = $spaces},";
    $id++;
}
chop $content;
$content .= "\n);\n\n";

$id=1;
while(my ($name, $info) = each(%hotels)) {
    my ($operator, $description, $single, $double, $family) = @$info;
    next unless $operator;
    $content .= "Operator $operator = new Operator{ Id = $id, Name = \"$operator\" };\n";
    $id++;
}

$content .= "\n\n";

$id=1;
while(my ($name, $info) = each(%hotels)) {
    my ($operator, $description, $single, $double, $family) = @$info;
    (my $var = $name) =~ s/[\s,;]//g;
    $content .= "Hotel $var = new Hotel{Id = $id, Name = \"$name\", Description = \"$description\", OperatorId = $operator.Id};\n";
    $id++;
}

$content .= "\n\n";

$content .= "modelBuilder.Entity<Operator>().HasData(";
while(my ($name, $info) = each(%hotels)) {
    my ($operator, $description, $single, $double, $family) = @$info;
    next unless $operator;
    $content .= "\n\t$operator,";
}
chop $content;
$content .= "\n);\n\n";

$content .= "modelBuilder.Entity<Hotel>().HasData(";
while(my ($name, $info) = each(%hotels)) {
    my ($operator, $description, $single, $double, $family) = @$info;
    (my $var = $name) =~ s/[\s,;]//g;
    $content .= "\n\t$var,";
}
chop $content;
$content .= "\n);\n\n";

$content .= "modelBuilder.Entity<Room>().HasData(\n";
$id=1;
while(my ($name, $info) = each(%hotels)) {
    my ($operator, $description, $single, $double, $family) = @$info;
    (my $var = $name) =~ s/[\s,;]//g;
    $content .= "\n\tnew Room{Id = @{[$id++]}, Name = \"Single Bed\", HotelId = $var.Id, Price = @{[$single*100]}, Spaces = $room_spaces, PackageDiscount = 10},";
    $content .= "\n\tnew Room{Id = @{[$id++]}, Name = \"Double Bed\", HotelId = $var.Id, Price = @{[$double*100]}, Spaces = $room_spaces, PackageDiscount = 20},";
    $content .= "\n\tnew Room{Id = @{[$id++]}, Name = \"Family Suite\", HotelId = $var.Id, Price = @{[$family*100]}, Spaces = $room_spaces, PackageDiscount = 40},";
}
chop $content;
$content .= "\n);";

$content =~ s/\n/\n\t\t/g;


open FH, '>', $out_file_name;

printf FH $template, $content;

close FH;