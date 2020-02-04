<?php
use PhpOffice\PhpWord\IOFactory;
require_once 'vendor/autoload.php';

$wordPdf = PhpOffice\PhpWord\IOFactory::load('templateresult.docx');
$pdfWriter = PhpOffice\PhpWord\IOFactory::createWriter($wordPdf , 'PDF');    
$pdfWriter->save("templateresult.pdf");

?>