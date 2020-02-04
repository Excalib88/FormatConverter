<?php
use PhpOffice\PhpWord\IOFactory;
use Dompdf\Dompdf;

require_once 'vendor/autoload.php';

$method = $_SERVER["REQUEST_METHOD"];

if($method != "POST") {
    echo('Некорректный запрос!');
    return;
}

$templateProcessor = new \PhpOffice\PhpWord\TemplateProcessor('Template.docx');

$templateProcessor->setValue('d_num', $_POST["d_num"]);
$templateProcessor->setValue('d_date', '04.10.2014');
$templateProcessor->setValue('last_name', 'Никоненко');
$templateProcessor->setValue('имя', 'Сергей');
$templateProcessor->setValue('surname', 'Васильевич');
$templateProcessor->setValue('konstantin', 'сосите ХУЕЦ!');

$templateProcessor->saveAs('template312312.docx');

shell_exec('sudo unoconv -f pdf template312312.docx');

$attachment_location = $_SERVER["DOCUMENT_ROOT"] . '\template312312.docx';


if (file_exists($attachment_location)) {
    header($_SERVER["SERVER_PROTOCOL"]." 200 OK");
    header("Cache-Control: public"); // needed for internet explorer
    header("Content-Type: application/msword");
    header("Content-Transfer-Encoding: Binary");
    header("Content-Length:".filesize($attachment_location));
    header("Content-Disposition: attachment; filename=file.docx");
    readfile($attachment_location);
    echo($attachment_location);
}
else{
    echo('Файл не найден!');
}
?>