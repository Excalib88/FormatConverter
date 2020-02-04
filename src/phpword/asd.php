<?php
use PhpOffice\PhpWord\IOFactory;
require_once 'vendor/autoload.php';

$pdf = Gears\Pdf::convert('Template312312.docx', 'template312312.pdf');

?>