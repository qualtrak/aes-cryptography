#! /usr/bin/perl

use strict;
use warnings;
use 5.10.1;

use Crypt::CBC;
use MIME::Base64;

# my $key    = Crypt::CBC->random_bytes(32);  # assuming a 8-byte block cipher
# my $iv     = Crypt::CBC->random_bytes(16);

my $key  = "ZlVr9tCvXTN35g0Td82ZMLeKlH5pP3WiS2huAowBro0=";
my $iv   = "l3Svi3wnA4aGyUqs2KBAnw==";

#my $key_encoded = encode_base64($key);
#my $iv_encoded = encode_base64($iv);

#print "Key: $key_encoded IV: $iv_encoded \n";

my $cipher = Crypt::CBC->new({
    key         => decode_base64($key),  # 256 bits
    cipher      => "Crypt::Rijndael",
    iv          => decode_base64($iv), # 128 bits
    literal_key => 1,
    header      => "none",
    keysize     => 32 # 256/8
});
 
my $encrypted = $cipher->encrypt("qualtrak");
 
# base64 encode so we can store in db
my $encrypted_base = encode_base64($encrypted);
 
# remove trailing newline inserted by encode_base64
chomp($encrypted_base);
 
print "encrypted result: $encrypted_base \n";

my $decrypted = $cipher->decrypt($encrypted);

print "decrypted result: $decrypted \n";
