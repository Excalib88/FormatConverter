<?php
use PhpOffice\PhpWord\IOFactory;

require_once 'vendor/autoload.php';

$method = $_SERVER["REQUEST_METHOD"];

if($method != "POST") {
    echo('Некорректный запрос!');
    return;
}
class Dictionary {
    public $Key;
    public $Value;
}

$templateProcessor = new \PhpOffice\PhpWord\TemplateProcessor('Template.docx');

$schemas = json_decode($_POST["schema"]);
$mapper = new JsonMapper();
$dictArray = $mapper->mapArray($schemas, new ArrayObject(), 'Dictionary');

foreach($dictArray as $dict) {
    $templateProcessor->setValue($dict->Key, $dict->Value);
}

$templateProcessor->saveAs('templateResult.docx');

$attachment_location = $_SERVER["DOCUMENT_ROOT"] . '/templateResult.docx';


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
    echo("\n Файл не найден!");
}
?>